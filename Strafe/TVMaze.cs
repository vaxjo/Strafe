﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Strafe {
    // http://www.tvmaze.com/api
    public class TVMaze {

        /// <summary> Query TV Maze and, ideally, return the show's canonical name and id. </summary>
        public static Tuple<int, string> GetShowName(string fileShowName) {

            ShowMapping cache = StrafeForm.Config.ShowMappings.FirstOrDefault(o => o.FileShowName == fileShowName && o.TVSource == ShowMapping.TVSources.TVMaze);
            if (cache != null) return new Tuple<int, string>(cache.TVMazeId, cache.CanonicalShowName);

            var result = GetShowName2(fileShowName);
            StrafeForm.Config.SetTVMazeMapping(fileShowName, result.Item2, result.Item1);
            StrafeForm.Config.Save();

            return new Tuple<int, string>(result.Item1, result.Item2);
        }

        /// <summary> Try several approaches to find the show. </summary>
        protected static Tuple<int, string> GetShowName2(string fileShowName) {
            // after each failure, lop off the end of the name and try again
            string slowlyReducingFileName = fileShowName.Trim();
            while (slowlyReducingFileName.Length > 0) {
                CacheItem singlesearchCache = StrafeForm.Cache.Get("http://api.tvmaze.com/singlesearch/shows?q=" + slowlyReducingFileName);
                JSONResponse singlesearch = singlesearchCache.JSONResponse;

                if (singlesearch.HTTPStatus == HttpStatusCode.OK) return new Tuple<int, string>((int) singlesearch.JSON.id, (string) singlesearch.JSON.name);

                if (singlesearch.HTTPStatus != HttpStatusCode.NotFound) {
                    StrafeForm.Log("Unknown TVMaze http status: " + singlesearch.HTTPStatus);
                    throw new TVMazeException("TVMaze: unknown error");
                }

                int lastSpace = slowlyReducingFileName.LastIndexOf(' ');
                slowlyReducingFileName = slowlyReducingFileName.Substring(0, lastSpace).Trim();
            }

            StrafeForm.Log("Couldn't find show on TVMaze: http://api.tvmaze.com/singlesearch/shows?q=" + fileShowName);
            throw new TVMazeException("TVMaze: couldn't find show");
        }

        public static string GetEpisodeName(int tvMazeId, int season, int episode) {
            CacheItem episodesCache = StrafeForm.Cache.Get("http://api.tvmaze.com/shows/" + tvMazeId + "/episodes?specials=1");
            JSONResponse episodes = episodesCache.JSONResponse;

            if (episodes.HTTPStatus != HttpStatusCode.OK) {
                StrafeForm.Log("Couldn't get episode list on TVMaze: http://api.tvmaze.com/shows/" + tvMazeId + "/episodes?specials=1");
                throw new TVMazeException("TVMaze: couldn't get episode list");
            }

            foreach (var item in episodes.JSON) {
                if (((int?) item.season ?? 0) == season && ((int?) item.number ?? 0) == episode) return (string) item.name;
            }

            StrafeForm.Log("Couldn't find episode on TVMaze: http://api.tvmaze.com/shows/" + tvMazeId + "/episodes?specials=1");
            throw new TVMazeException("TVMaze: couldn't find episode");
        }

        public static List<string> GetEpisodeList(int tvMazeId) {
            CacheItem episodesCache = StrafeForm.Cache.Get("http://api.tvmaze.com/shows/" + tvMazeId + "/episodes?specials=1");
            JSONResponse episodes = episodesCache.JSONResponse;

            if (episodes.HTTPStatus != HttpStatusCode.OK) {
                StrafeForm.Log("Couldn't find episode on TVMaze: http://api.tvmaze.com/shows/" + tvMazeId + "/episodes?specials=1");
                throw new TVMazeException("TVMaze: couldn't get episode list");
            }

            List<string> episodeNames = new List<string>();
            foreach (var item in episodes.JSON) {
                int season = (int?) item.season ?? 0;
                int number = (int?) item.number ?? 0;
                string name = item.name;
                episodeNames.Add(season + "," + number + "," + name);
            }
            return episodeNames;
        }

        public static List<string> GetShowList(string search) {
            CacheItem episodesCache = StrafeForm.Cache.Get("http://api.tvmaze.com/search/shows?q=" + search);
            JSONResponse episodes = episodesCache.JSONResponse;

            if (episodes.HTTPStatus != HttpStatusCode.OK) {
                throw new TVMazeException("TVMaze: no matches");
            }

            List<string> showNames = new List<string>();
            foreach (var item in episodes.JSON) {
                string name = item.show.name;
                int id = item.show.id;
                string premiered = (string) item.show.premiered;
                showNames.Add(id + "," + (string.IsNullOrWhiteSpace(premiered) ? "" : premiered.Substring(0, 4)) + "," + name);
            }
            return showNames;
        }
    }

    public class TVMazeException : Exception {
        public TVMazeException() { }
        public TVMazeException(string message) : base(message) { }
        public TVMazeException(string message, Exception inner) : base(message, inner) { }
    }
}
