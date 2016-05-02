using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using PIN.Core.Packages;
using ShellProgressBar;
using static PIN.Core.Language;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace PIN.Core.Managers
{
    class Manager
    {
        #region Lists

        public List<IAP> IgnoredList { get; set; } = new List<IAP>();
        public List<IAP> InstallList { get; set; }
        public List<IAP> InvalidPackages { get; set; }

        #endregion

        private Logger NLogger = LogManager.GetCurrentClassLogger();
        public ProgressBar ProgressBar { get; set; }       
        private Arguments Arguments { get; }


        public event ManagerProgress ProgressChanged;
        internal delegate void ManagerProgress(List<string> data, NotificationType notificationType);

        public Manager(Arguments arguments)
        {
            NLogger.Trace("New manager instantiated.");

            Arguments = arguments;
            ValidateInstallations();

            NLogger.Info($"Packages loaded. Valid [{InstallList.Count}] Ignored [{IgnoredList.Count}] Invalid [{InvalidPackages.Count}]");

            if (InstallList.Count == 0)
            {
                Utils.WriteError(CurrentLanguage.InstallationNoPackageFound);
                ProgressChanged?.Invoke(null, NotificationType.Programdone);
            }
        }

        /// <summary>
        /// Starts all the installations.
        /// </summary>
        /// <param name="args">if set to <c>true</c> then the application will be executed with all the designed arguments.</param>
        public void StartInstallation(bool args = true)
        {
            if(InstallList.Count == 0 || Arguments.Value.ContainsKey("update")) return;

            ProgressBar = new ProgressBar(InstallList.Count, "", new ProgressBarOptions
            {
                ProgressBarOnBottom = true,
                CollapseWhenFinished = true,
                BackgroundColor = ConsoleColor.Gray,
                ForeGroundColor = ConsoleColor.White
            });

            foreach (IAP iap in InstallList)
            {             
                NLogger.Debug($"Beginning {iap.Packagename} installation.");

                ProgressBar.UpdateMessage(string.Format(CurrentLanguage.InstallationProgress, iap.Packagename));
                iap.Install(args);
                ProgressBar.Tick();
            }
        }

        /// <summary>
        /// Validates the installations.
        /// </summary>
        /// <returns></returns>
        void ValidateInstallations()
        {
            InstallList = new Scanner().Scan();
            foreach (IAP error in InstallList.Where(iap => !iap.ValidateInstall()).ToList()) InstallList.Remove(error);

            InvalidPackages = InstallList.Where(iap => !iap.ValidateInstall()).ToList();

            foreach (IAP invalidPackage in InvalidPackages)        
                NLogger.Debug($"{invalidPackage.Packagename} is invalid.");
            

            if (InvalidPackages.Count > 0) { ProgressChanged?.Invoke(InvalidPackages.Select(invalid => invalid.Packagename).ToList(), NotificationType.Installaltioninvalid); }


            if (Arguments.Value.ContainsKey("ignore"))
            {
                foreach (var packagename in Arguments.Value.SelectMany(aa => aa.Value))
                {
                    IgnoredList?.Add(InstallList.Find(x => string.Equals(x.Packagename, packagename, StringComparison.CurrentCultureIgnoreCase)));
                    NLogger.Debug($"{packagename} is ignored.");
                }

                InstallList = Utils.Remove(InstallList, IgnoredList);
            }

            InstallList.Sort();
        }
    }
}
