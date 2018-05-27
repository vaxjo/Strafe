using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Strafe {
    // I am a bit worried about stuffing all the cached JSON responses into one cache file. 
    //  if it gets big, performance on-load will suffer. 
    //  if it gets really big, our memory footsprint might become unacceptably large.
    // we could make discrete files for each cache request (eg, "/cache/tvmaze-search-search string.json")
    public class Cache {
        protected List<CacheItem> CacheItems;

        protected FileInfo CacheFile {
            get {
                FileInfo entry = new FileInfo(System.Reflection.Assembly.GetCallingAssembly().Location);
                return new FileInfo(Path.Combine(entry.Directory.FullName, @"strafe.cache.json"));
            }
        }

        public Cache() {
            CacheItems = new List<CacheItem>();
            StrafeForm.Log("Cache system " + (StrafeForm.Config.CacheEnabled ? "enabled" : "disabled"));
            if (!StrafeForm.Config.CacheEnabled) return;

            StrafeForm.Log("Loading cache file");
            if (!CacheFile.Exists) return;

            CacheItems = (List<CacheItem>) JsonConvert.DeserializeObject<List<CacheItem>>(File.ReadAllText(CacheFile.FullName));
            CacheItems.RemoveAll(o => DateTime.Now.Subtract(o.LastUsed).TotalDays > StrafeForm.Config.CacheExpiration); // don't anything that's expired
            StrafeForm.Log("Cache loaded, " + CacheItems.Count + " items");
        }

        public CacheItem Get(string url) {
            // if we don't have it - go get it
            if (!CacheItems.Any(o => o.Url == url)) {
                StrafeForm.Log("Adding new cache item: " + url);
                CacheItems.Add(new CacheItem(url));
            }

            CacheItem hit = CacheItems.Single(o => o.Url == url);
            hit.LastUsed = DateTime.Now;
            return hit;
        }

        public void Delete(string url) {
            CacheItems.RemoveAll(o => o.Url == url);
        }

        public void Clear() {
            CacheItems = new List<CacheItem>();
        }

        public void Save() {
            if (!StrafeForm.Config.CacheEnabled) {
                CacheFile.Delete();
                return;
            }

            StrafeForm.Log("Saving cache file");
            if (CacheItems.Count == 0) {
                CacheFile.Delete();
                return;
            }

            File.WriteAllText(CacheFile.FullName, JsonConvert.SerializeObject(CacheItems));
        }
    }

    public class CacheItem {
        public string Url;
        public DateTime LastUsed;
        public JSONResponse JSONResponse;

        public CacheItem(string url) {
            Url = url;
            LastUsed = DateTime.Now;
            JSONResponse = JSONResponse.Get(url);
        }
    }
}