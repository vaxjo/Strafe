using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Strafe {
    public class Episode {
        public string RawShowName;
        public string ShowName, EpisodeName;
        public int Season, EpisodeNumber;
        public int TVMazeId;
        public string Error;

        public Episode(string filename) {
            // we don't need the full path
            if (filename.Contains("\\")) filename = filename.Split('\\').ToList().Last();
            StrafeForm.Log("Processing [" + filename + "]");

            RawShowName = System.IO.Path.GetFileNameWithoutExtension(filename).ToLower();
            string seSubstring = ExtractSE(filename); // eg, S01E01
            if (seSubstring == "") {
                StrafeForm.Log("Can't determine season/episode number");
                Error = "Can't determine season/episode number.";

            } else {
                StrafeForm.Log("Found SE string: \"" + seSubstring + "\"");
                // assume everything before the SE string is the show name
                RawShowName = filename.Substring(0, filename.IndexOf(seSubstring));
            }

            RawShowName = Regex.Replace(RawShowName, @"'", ""); // remove some punctuation
            RawShowName = Regex.Replace(RawShowName, @"\W", " "); // convert others to spaces
            RawShowName = Regex.Replace(RawShowName, @"\s+", " "); // consolidate white space
            RawShowName = RawShowName.Trim();
            StrafeForm.Log("Raw show name: \"" + RawShowName + "\"");

            // TODO: talk to theTVDB's API to get the canonical show name and episode name - https://api.thetvdb.com/swagger

            try {
                var tvmazeResult = TVMaze.GetShowName(RawShowName);
                TVMazeId = tvmazeResult.Item1;
                ShowName = tvmazeResult.Item2;

                if (Season > 0 && EpisodeNumber > 0) EpisodeName = TVMaze.GetEpisodeName(TVMazeId, Season, EpisodeNumber);

            } catch (TVMazeException tvExc) {
                Error = tvExc.Message;
            }
        }

        /// <summary> Extract the season/episode number and return the matched substring. </summary>
        protected string ExtractSE(string filename) {
            Match match = Regex.Match(filename, @"s(\d+)E(\d+)", RegexOptions.IgnoreCase); // try for s00e00
            if (match.Success) {
                Season = Convert.ToInt32(match.Groups[1].Value);
                EpisodeNumber = Convert.ToInt32(match.Groups[2].Value);
                return match.Value;
            }

            match = Regex.Match(filename, @"(\d+)x(\d+)", RegexOptions.IgnoreCase); // try for 00x00
            if (match.Success) {
                Season = Convert.ToInt32(match.Groups[1].Value);
                EpisodeNumber = Convert.ToInt32(match.Groups[2].Value);
                return match.Value;
            }

            match = Regex.Match(filename, @"season\s(\d+).*episode\s(\d+)", RegexOptions.IgnoreCase); // try for "season 01 episode 01"
            if (match.Success) {
                Season = Convert.ToInt32(match.Groups[1].Value);
                EpisodeNumber = Convert.ToInt32(match.Groups[2].Value);
                return match.Value;
            }

            return "";
        }
    }
}
