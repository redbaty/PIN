using System.Collections.Generic;
using System.Linq;

namespace PIN.Core.jModels
{
    static class Translation
    {
        public static string InstallationProgressBarFormat { get; set; }//"Installing {0} of {1}. ({2}%)"
        public static string InstallListLoaded { get; set; }//"Loading installation list."
        public static string InstallationSuccess { get; set; }//"Sucess !"
        public static string InstallationPackageInvalid { get; set; }//""Pacotes invalidos : {0}"
        public static string DownloadProgressBarFormat { get; set; }//"Downloaded {0:F}MB of {1:F}MB. ({2}%)"

        public static void LoadTranslation(string language)
        {
            List<string> AvailableLanguages = new Scanner().Find("*.lang").ToList();
            if (AvailableLanguages.Contains(language)) Load(AvailableLanguages.Find(x => x.Contains(string.Format("{0}.lang", language))));
            else
                LoadDefault();
        }

        public static void LoadDefault()
        {
            InstallationProgressBarFormat = "Installing {0} of {1}. ({2}%)";
            InstallListLoaded = "Installation list loaded !.";
            InstallationSuccess = "Success !";
            InstallationPackageInvalid = "Invalid packages {0}";
            DownloadProgressBarFormat = "Downloaded {0:F}MB of {1:F}MB. ({2}%)";
        }

        public static void Load(string path)
        {
            
        }
    }
}
