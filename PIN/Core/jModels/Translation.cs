using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PIN.Core.jModels
{
    static class Translation
    {
        public static string InstallationProgressBarFormat { get; set; }//"Installing {0} of {1}. ({2}%)"
        public static string InstallListLoaded { get; set; }//"Loading installation list."
        public static string InstallationSuccess { get; set; }//"Sucess !"
        public static string InstallationPackageInvalid { get; set; }//""Pacotes invalidos : {0}"
        public static string DownloadProgressBarFormat { get; set; }//"Downloaded {0:F}MB of {1:F}MB. ({2}%)"
        public static string GithubInformation { get; set; } //"Help us at github : {0}"

        public static void LoadTranslation(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    Load(JsonConvert.DeserializeObject<TranslationSave>(File.ReadAllText(path)));
                }
                catch (Exception)
                {
                    LoadDefault();
                }
            }
            else
                LoadDefault();
        }

        public static void LoadDefault()
        {
            InstallationProgressBarFormat = "Installing {0} of {1}. ({2}%)";
            InstallListLoaded = "Installation list loaded !.";
            InstallationSuccess = "Success !";
            InstallationPackageInvalid = "Invalid packages: {0}";
            DownloadProgressBarFormat = "Downloaded {0:F}MB of {1:F}MB. ({2}%)";
            GithubInformation = "Help us at github : {0}";
        }

        private static void Load(TranslationSave trsave)
        {
            InstallationProgressBarFormat = trsave.InstallationProgressBarFormat;
            InstallListLoaded = trsave.InstallListLoaded;
            InstallationSuccess = trsave.InstallationSuccess;
            InstallationPackageInvalid = trsave.InstallationPackageInvalid;
            DownloadProgressBarFormat = trsave.DownloadProgressBarFormat;
            GithubInformation = trsave.GithubInformation;
        }


        public class TranslationSave
        {
            public string InstallationProgressBarFormat { get; set; }//"Installing {0} of {1}. ({2}%)"
            public string InstallListLoaded { get; set; }//"Loading installation list."
            public string InstallationSuccess { get; set; }//"Sucess !"
            public string InstallationPackageInvalid { get; set; }//""Pacotes invalidos : {0}"
            public string DownloadProgressBarFormat { get; set; }//"Downloaded {0:F}MB of {1:F}MB. ({2}%)"
            public string GithubInformation { get; set; } //"Help us at github : {0}"

            public TranslationSave()
            {
                InstallationProgressBarFormat = "Installing {0} of {1}. ({2}%)";
                InstallListLoaded = "Installation list loaded !.";
                InstallationSuccess = "Success !";
                InstallationPackageInvalid = "Invalid packages: {0}";
                DownloadProgressBarFormat = "Downloaded {0:F}MB of {1:F}MB. ({2}%)";
                GithubInformation = "Help us at github : {0}";
            }
        }
    }
}
