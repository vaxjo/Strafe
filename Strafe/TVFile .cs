using System.IO;

namespace Strafe {
    public class TVFile {
        public enum Actions { Ignore, Delete, Rename, Error }

        public FileInfo OriginalFile;
        public Actions Action;
        public string ErrorMessage;
        public Episode Episode;

        public string Extension_Trimmed => OriginalFile.Extension.Trim('.');

        /// <summary> Returns true if we need to consult some APIs about this tv show. </summary>
        public bool NeedsProcessing => (Action == TVFile.Actions.Rename && Episode == null);

        public TVFile(string filepath) : this(new FileInfo(filepath)) { }

        public TVFile(FileInfo file) {
            OriginalFile = file;
            Action = GetDefaultAction();
        }

        /// <summary> This involves API calls. </summary>
        public void PopulateEpisode() {
            Episode = new Episode(OriginalFile.FullName);

            if (!string.IsNullOrWhiteSpace(Episode.Error)) {
                ErrorMessage = Episode.Error;
                Action = TVFile.Actions.Error;
            }
        }

        public Actions GetDefaultAction() {
            Config config = StrafeForm.Config;
            if (config.RenameExtensions.Contains(Extension_Trimmed)) return Actions.Rename;
            if (config.DeleteExtensions.Contains(Extension_Trimmed)) return Actions.Delete;
            return Actions.Ignore;
        }

        public string GetNewFilepath() {
            if (Episode == null) return "";

            // fileFormat = "{showName}\{showName} - s{ss}e{ee} - {episodeName}"
            string formattedName = StrafeForm.Config.FileFormat;

            formattedName = formattedName.Replace("{showName}", StrafeForm.Config.GetFileSafeName(Episode.ShowName));
            formattedName = formattedName.Replace("{episodeName}", StrafeForm.Config.GetFileSafeName(Episode.EpisodeName));
            formattedName = formattedName.Replace("{s}", Episode.Season.ToString("D1"));
            formattedName = formattedName.Replace("{ss}", Episode.Season.ToString("D2"));
            formattedName = formattedName.Replace("{sss}", Episode.Season.ToString("D3"));
            formattedName = formattedName.Replace("{e}", Episode.EpisodeNumber.ToString("D1"));
            formattedName = formattedName.Replace("{ee}", Episode.EpisodeNumber.ToString("D2"));
            formattedName = formattedName.Replace("{eee}", Episode.EpisodeNumber.ToString("D3"));

            return formattedName + OriginalFile.Extension;
        }

    }
}
