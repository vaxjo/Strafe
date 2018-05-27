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
        public bool Logging, ReplaceExistingFiles;
        public List<ShowMapping> ShowMappings;
        public bool CacheEnabled;
        public int CacheExpiration;

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
                using (StreamReader reader = new StreamReader(stream))
                    File.WriteAllText(ConfigFile.FullName, reader.ReadToEnd());
            }

            XElement xmlConfig = XElement.Load(ConfigFile.FullName);

            TVShowRootFilepath = xmlConfig.Attribute("tvShowRoot").Value;
            FileFormat = xmlConfig.Attribute("fileFormat").Value;
            Logging = xmlConfig.Attribute("logging").Value.ToLower() == "true";

            DeleteExtensions = xmlConfig.Element("Actions").Attribute("delete").Value.Split(',').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
            RenameExtensions = xmlConfig.Element("Actions").Attribute("rename").Value.Split(',').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
            ReplaceExistingFiles = Convert.ToBoolean(xmlConfig.Element("Actions").Attribute("replaceExistingFiles").Value);

            Replacement_Asterisk = xmlConfig.Element("Replacements").Attribute("asterisk").Value;
            Replacement_Backslash = xmlConfig.Element("Replacements").Attribute("backslash").Value;
            Replacement_Colon = xmlConfig.Element("Replacements").Attribute("colon").Value;
            Replacement_DoubleQuotes = xmlConfig.Element("Replacements").Attribute("doubleQuotes").Value;
            Replacement_ForwardSlash = xmlConfig.Element("Replacements").Attribute("forwardSlash").Value;
            Replacement_GreaterThan = xmlConfig.Element("Replacements").Attribute("greaterThan").Value;
            Replacement_LessThan = xmlConfig.Element("Replacements").Attribute("lessThan").Value;
            Replacement_Pipe = xmlConfig.Element("Replacements").Attribute("pipe").Value;
            Replacement_QuestionMark = xmlConfig.Element("Replacements").Attribute("questionMark").Value;

            CacheEnabled = Convert.ToBoolean(xmlConfig.Element("Cache").Attribute("enabled").Value);
            CacheExpiration = Convert.ToInt32(xmlConfig.Element("Cache").Attribute("expiration").Value);

            ShowMappings = new List<ShowMapping>();
            foreach (XElement xmlCache in xmlConfig.Elements("ShowMapping")) {
                ShowMappings.Add(new ShowMapping() {
                    TVSource = (ShowMapping.TVSources) Enum.Parse(typeof(ShowMapping.TVSources), xmlCache.Attribute("source").Value),
                    FileShowName = xmlCache.Attribute("fileShowName").Value,
                    SourceId = xmlCache.Attribute("sourceId").Value
                });
            }
        }

        /// <summary> Add or update cached show item. (Does not call Save().) </summary>
        public void SetTVMazeMapping(string fileShowName, int sourceId) {
            ShowMappings.RemoveAll(o => o.FileShowName.ToLower() == fileShowName.ToLower());
            ShowMappings.Add(new ShowMapping() { FileShowName = fileShowName.ToLower(), SourceId = sourceId.ToString(), TVSource = ShowMapping.TVSources.TVMaze });
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

            config.Add(new XElement("Cache",
               new XAttribute("enabled", CacheEnabled.ToString()),
               new XAttribute("expiration", CacheExpiration.ToString())
           ));

            config.Add(new XElement("Actions",
                new XAttribute("rename", string.Join(",", RenameExtensions)),
                new XAttribute("delete", string.Join(",", DeleteExtensions)),
                new XAttribute("replaceExistingFiles", ReplaceExistingFiles.ToString())
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

            foreach (ShowMapping mapping in ShowMappings) {
                config.Add(new XElement("ShowMapping",
                    new XAttribute("source", mapping.TVSource),
                    new XAttribute("fileShowName", mapping.FileShowName),
                    new XAttribute("sourceId", mapping.SourceId)
                ));
            }

            config.Save(ConfigFile.FullName);
        }
    }

    /// <summary> Stores a local mapping of a filename substring to a canonical TV show. </summary>
    public class ShowMapping {
        public enum TVSources { TVMaze }

        public string FileShowName, SourceId;
        public TVSources TVSource;

        public int TVMazeId => Convert.ToInt32(SourceId);
    }
}
