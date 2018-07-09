using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Strafe {
    // http://www.tvmaze.com/api
    public class TVMaze {

        /// <summary> Query TV Maze and, ideally, return the show's canonical name and id. </summary>
        public static TVMaze_Show GetShowName(string fileShowName) {
            ShowMapping showMapping = StrafeForm.Config.ShowMappings.FirstOrDefault(o => o.FileShowName.ToLower() == fileShowName.ToLower() && o.TVSource == ShowMapping.TVSources.TVMaze);
            if (showMapping != null) {
                CacheItem showCache = StrafeForm.Cache.Get("http://api.tvmaze.com/shows/" + showMapping.TVMazeId);
                return new TVMaze_Show(showCache.JSONResponse.JSON);
            }

            TVMaze_Show result = GetShowName2(fileShowName);
            StrafeForm.Config.SetTVMazeMapping(fileShowName, result.TVMazeId);
            StrafeForm.Config.Save();

            return result;
        }

        /// <summary> Try several approaches to find the show. </summary>
        protected static TVMaze_Show GetShowName2(string fileShowName) {
            // after each failure, lop off the end of the name and try again
            string slowlyReducingFileName = fileShowName.Trim();
            while (slowlyReducingFileName.Length > 0) {
                CacheItem singlesearchCache = StrafeForm.Cache.Get("http://api.tvmaze.com/singlesearch/shows?q=" + slowlyReducingFileName);
                JSONResponse singlesearch = singlesearchCache.JSONResponse;

                if (singlesearch.HTTPStatus == HttpStatusCode.OK) return new TVMaze_Show(singlesearch.JSON);

                if (singlesearch.HTTPStatus != HttpStatusCode.NotFound) {
                    StrafeForm.Log("Unknown TVMaze http status: " + singlesearch.HTTPStatus);
                    throw new TVMazeException("TVMaze: unknown error");
                }

                int lastSpace = slowlyReducingFileName.LastIndexOf(' ');
                slowlyReducingFileName = lastSpace >= 0 ? slowlyReducingFileName.Substring(0, lastSpace).Trim() : "";
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
            throw new TVMazeException("TVMaze: couldn't find episode in list");
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

    public class TVMaze_Show {
        public int TVMazeId;
        public string ShowName, Year;

        public TVMaze_Show(dynamic showJson) {
            TVMazeId = showJson.id;
            ShowName = showJson.name;
            Year = (string) showJson.premiered; // 1983-05-18
            Year = (string.IsNullOrWhiteSpace(Year) ? "" : Year.Substring(0, 4));
        }
    }

    public class TVMazeException : Exception {
        public TVMazeException() { }
        public TVMazeException(string message) : base(message) { }
        public TVMazeException(string message, Exception inner) : base(message, inner) { }
    }
}
