using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Strafe {
    public class Config {
        public string TVShowRootFilepath;
        public List<string> DeleteExtensions, RenameExtensions;
        public string FileFormat;
        public bool Logging;
        public List<CachedShow> CachedShows;

        public string Replacement_DoubleQuotes, Replacement_LessThan, Replacement_GreaterThan, Replacement_Pipe, Replacement_Colon, Replacement_Asterisk, Replacement_Backslash, Replacement_QuestionMark, Replacement_ForwardSlash;

        public DirectoryInfo TVShowRoot => new DirectoryInfo(TVShowRootFilepath);

        private FileInfo ConfigFile {
            get {
                FileInfo entry = new FileInfo(System.Reflection.Assembly.GetCallingAssembly().Location);
                return new FileInfo(Path.Combine(entry.Directory.FullName, @"strafe.config.xml"));
            }
        }

        public Config() {
            if (!ConfigFile.Exists) {
                // get default from embedded resource
                using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Strafe.default.config.xml"))
                using (StreamReader reader = new StreamReader(stream)) {
                    File.WriteAllText(ConfigFile.FullName, reader.ReadToEnd());
                }
            }

            XElement xmlConfig = XElement.Load(ConfigFile.FullName);

            TVShowRootFilepath = xmlConfig.Attribute("tvShowRoot").Value;
            FileFormat = xmlConfig.Attribute("fileFormat").Value;
            Logging = xmlConfig.Attribute("logging").Value.ToLower() == "true";

            DeleteExtensions = xmlConfig.Element("Actions").Attribute("delete").Value.Split(',').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
            RenameExtensions = xmlConfig.Element("Actions").Attribute("rename").Value.Split(',').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();

            Replacement_Asterisk = xmlConfig.Element("Replacements").Attribute("asterisk").Value;
            Replacement_Backslash = xmlConfig.Element("Replacements").Attribute("backslash").Value;
            Replacement_Colon = xmlConfig.Element("Replacements").Attribute("colon").Value;
            Replacement_DoubleQuotes = xmlConfig.Element("Replacements").Attribute("doubleQuotes").Value;
            Replacement_ForwardSlash = xmlConfig.Element("Replacements").Attribute("forwardSlash").Value;
            Replacement_GreaterThan = xmlConfig.Element("Replacements").Attribute("greaterThan").Value;
            Replacement_LessThan = xmlConfig.Element("Replacements").Attribute("lessThan").Value;
            Replacement_Pipe = xmlConfig.Element("Replacements").Attribute("pipe").Value;
            Replacement_QuestionMark = xmlConfig.Element("Replacements").Attribute("questionMark").Value;

            CachedShows = new List<CachedShow>();
            foreach (XElement xmlCache in xmlConfig.Elements("Cache")) {
                CachedShows.Add(new CachedShow() {
                    TVSource = (CachedShow.TVSources) Enum.Parse(typeof(CachedShow.TVSources), xmlCache.Attribute("source").Value),
                    FileShowName = xmlCache.Attribute("fileShowName").Value,
                    CanonicalShowName = xmlCache.Attribute("canonicalShowName").Value,
                    TVMazeId = Convert.ToInt32(xmlCache.Attribute("tvMazeId").Value) });
            }
        }

        /// <summary> Add or update cached show item. (Does not call Save().) </summary>
        public void SetTVMazeCacheItem(string fileShowName, string canonicalShowName, int tvMazeId) {
            CachedShows.RemoveAll(o => o.FileShowName == fileShowName);
            CachedShows.Add(new CachedShow() { CanonicalShowName = canonicalShowName, FileShowName = fileShowName, TVMazeId = tvMazeId, TVSource = CachedShow.TVSources.TVMaze });
        }

        /// <summary> Remove and replace illegal file name characters. </summary>
        public string GetFileSafeName(string filename) {
            filename = filename.Replace("*", Replacement_Asterisk);
            filename = filename.Replace(":", Replacement_Colon);
            filename = filename.Replace("\"", Replacement_DoubleQuotes);
            filename = filename.Replace("/", Replacement_ForwardSlash);
            filename = filename.Replace("\\", Replacement_Backslash);
            filename = filename.Replace(">", Replacement_GreaterThan);
            filename = filename.Replace("<", Replacement_LessThan);
            filename = filename.Replace("|", Replacement_Pipe);
            filename = filename.Replace("?", Replacement_QuestionMark);

            filename = filename.Replace("  ", " ");

            return filename;
        }

        public void Save() {
            XElement config = new XElement("Strafe",
                new XAttribute("tvShowRoot", TVShowRootFilepath),
                new XAttribute("fileFormat", FileFormat),
                new XAttribute("logging", Logging.ToString())
            );

            config.Add(new XElement("Actions",
                new XAttribute("rename", string.Join(",", RenameExtensions)),
                new XAttribute("delete", string.Join(",", DeleteExtensions))
            ));

            config.Add(new XElement("Replacements",
                new XAttribute("asterisk", Replacement_Asterisk),
                new XAttribute("backslash", Replacement_Backslash),
                new XAttribute("colon", Replacement_Colon),
                new XAttribute("doubleQuotes", Replacement_DoubleQuotes),
                new XAttribute("forwardSlash", Replacement_ForwardSlash),
                new XAttribute("greaterThan", Replacement_GreaterThan),
                new XAttribute("lessThan", Replacement_LessThan),
                new XAttribute("pipe", Replacement_Pipe),
                new XAttribute("questionMark", Replacement_QuestionMark)
            ));

            foreach (CachedShow cachedShow in CachedShows) {
                config.Add(new XElement("Cache",
                    new XAttribute("source", cachedShow.TVSource),
                    new XAttribute("fileShowName", cachedShow.FileShowName),
                    new XAttribute("canonicalShowName", cachedShow.CanonicalShowName),
                    new XAttribute("tvMazeId", cachedShow.TVMazeId)
                ));
            }

            config.Save(ConfigFile.FullName);
        }
    }

    public class CachedShow {
        public enum TVSources { TVMaze }

        public string FileShowName, CanonicalShowName;
        public int TVMazeId;
        public TVSources TVSource;
    }
}
