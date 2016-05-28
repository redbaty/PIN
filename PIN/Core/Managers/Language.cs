using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using PIN.Core.Misc;

namespace PIN.Core.Managers
{
    class Language
    {
        public static Language CurrentLanguage { get; set; } = new Language();
        public static Language Deserialize(string source) => JsonConvert.DeserializeObject<Language>(source);

        public string InstallListLoaded { get; set; } = "Installation list loaded !.";
        public string InstallationSuccess { get; set; } = "All packages installed sucessfully !";
        public string InstallationPackageInvalid { get; set; } = "Invalid packages: {0}";
        public string InstallationNoPackageFound { get; set; } = "No valid packages found.";
        public string InstallationProgress { get; set; } = "Installing {0}";
        public string GithubInformation { get; set; } = "Help us at github : {0}";
        public string ProgramDone { get; set; } = "Press any key to exit...";
        
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static Logger NLogger = LogManager.GetCurrentClassLogger();



        /// <summary>
        /// Search for the desired translation.
        /// </summary>
        /// <param name="tr">The translation file name.</param>
        public static void SearchTranslation(string tr)
        {
            var scnResults = new Scanner().Find($"{tr}.tap");

            if (scnResults?.Length != 0)
            {
                Debug.Assert(scnResults != null, "scnResults != null");
                var source = File.ReadAllText(scnResults[0]);

                try
                {
                    CurrentLanguage = Deserialize(source);
                    NLogger.Debug($"Language is now set to {tr}");
                }
                catch (Exception)
                {
                    Failed(tr);
                }
            }
            else
                Failed(tr);
        }

        public static void SaveExample()
        {
            WriteInFormattedJson("example.tap",new Language());
        }

        public static void WriteInFormattedJson(string path, Language json)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, json);
            }
        }

        private static void Failed(string lg)
        {
            Utils.WriteError("Translation not found.\n");
            CurrentLanguage = new Language();
            NLogger.Debug($"Language [{lg}] was not found, english was loaded.");
        }
    }
}

