using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLineParser.Arguments;
using NLog;
using PIN.Core.Misc;
using PIN.Core.Packages;
using ShellProgressBar;
using static PIN.Core.Managers.Language;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace PIN.Core.Managers
{
    class Manager
    {
        public List<Package> IgnoredList { get; set; } = new List<Package>();
        public List<Package> InstallList { get; set; }
        public List<Package> InvalidPackages { get; set; }
        public ProgressBar ProgressBar { get; set; }

        private Logger NLogger = LogManager.GetCurrentClassLogger();

        public event ManagerProgress ProgressChanged;
        internal delegate void ManagerProgress(List<string> data, NotificationType notificationType);

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        public Manager(string[] arguments)
        {
            #region Arguments Parsing

            var parser = new CommandLineParser.CommandLineParser();
            var helpArgument = new SwitchArgument('h', "help", "Show these commands", false);
            var exampleArgument = new SwitchArgument('e', "example", "Create a package example", false);
            var noInstallArgument = new SwitchArgument('n', "noinstall", "Don't install any packages.", false);
            var updateArgument = new SwitchArgument('u', "update", "Update all pin packages.", false);
            var ignoreArgument = new ValueArgument<string>('i', "ignore", "Ignore the named packages") { ValueOptional = false };
            var downloadArgument = new ValueArgument<string>('d', "download", "Download packages from chocolatey.") { ValueOptional = false };

            parser.Arguments.Add(helpArgument);
            parser.Arguments.Add(noInstallArgument);
            parser.Arguments.Add(downloadArgument);
            parser.Arguments.Add(updateArgument);
            parser.Arguments.Add(ignoreArgument);
            parser.Arguments.Add(exampleArgument);

            #endregion

            try
            {
                parser.ParseCommandLine(arguments);
                Utils.WriteVersion();

                ValidateInstallations(ignoreArgument);

                NLogger.Info($"Packages loaded. Valid [{InstallList.Count}] Ignored [{IgnoredList.Count}] Invalid [{InvalidPackages.Count}]");

                if (InstallList.Count == 0)
                {
                    Utils.WriteError(CurrentLanguage.InstallationNoPackageFound);
                    ProgressChanged?.Invoke(null, NotificationType.Programdone);
                    return;
                }

                if (helpArgument.Value) parser.ShowUsage();
                if (exampleArgument.Value) Package.Example($"{Directory.GetCurrentDirectory()}\\Packages\\Example\\example{Package.FileType}");
                if (!string.IsNullOrEmpty(downloadArgument.Value)) Download(downloadArgument.Value);
                if (!noInstallArgument.Value) StartInstallation();
            }
            catch (Exception ex)
            {
                Utils.WriteError($"An error occurred while parsing arguments.\n{ex}");
            }
        }

        /// <summary>
        /// Starts all the installations.
        /// </summary>
        /// <param name="args">if set to <c>true</c> then the application will be executed with all the designed arguments.</param>
        public void StartInstallation(bool args = true)
        {
            if (InstallList.Count == 0) return;

            ProgressBar = new ProgressBar(InstallList.Count, "", new ProgressBarOptions
            {
                ProgressBarOnBottom = true,
                CollapseWhenFinished = true,
                BackgroundColor = ConsoleColor.Gray,
                ForeGroundColor = ConsoleColor.White
            });

            foreach (Package iap in InstallList)
            {
                NLogger.Debug($"Beginning {iap.Packagename} installation.");

                ProgressBar.UpdateMessage(string.Format(CurrentLanguage.InstallationProgress, iap.Packagename));
                iap.Install(args);
                ProgressBar.Tick();
            }
        }

        /// <summary>
        /// Downloads packages from the specified packagestring.
        /// </summary>
        /// <param name="packagestring">The packagestring.</param>
        private void Download(string packagestring)
        {
            if (!packagestring.Contains(";"))
            {
                try
                {
                    new Chocolatey(packagestring).Download();
                }
                catch (Exception ex)
                {
                    Utils.WriteError($"An error occurred while downloading {packagestring} - {ex}");
                }
            }
            else if (packagestring.Contains(";"))
            {
                foreach (var package in packagestring.Split(';'))
                {
                    try
                    {
                        new Chocolatey(package).Download();
                    }
                    catch (Exception ex)
                    {
                        Utils.WriteError($"An error occurred while downloading {package} - {ex}");
                    }
                }
            }

            ChocolateyDownloader.WaitAndDispose();
        }

        /// <summary>
        /// Validates the installations.
        /// </summary>
        /// <returns></returns>
        void ValidateInstallations(ValueArgument<string> IgnoredArgument)
        {
            InstallList = new Scanner().Scan();
            foreach (Package error in InstallList.Where(iap => !iap.ValidateInstall()).ToList())
                InstallList.Remove(error);

            InvalidPackages = InstallList.Where(iap => !iap.ValidateInstall()).ToList();

            foreach (Package invalidPackage in InvalidPackages)
                NLogger.Debug($"{invalidPackage.Packagename} is invalid.");


            if (InvalidPackages.Count > 0)
            {
                ProgressChanged?.Invoke(InvalidPackages.Select(invalid => invalid.Packagename).ToList(),
                    NotificationType.Installaltioninvalid);
            }

            InstallList.Sort();

            if (string.IsNullOrEmpty(IgnoredArgument?.Value)) return;

            foreach (var packagename in IgnoredArgument.Value.Split(';'))
            {
                IgnoredList?.Add(
                    InstallList.Find(
                        x => string.Equals(x.Packagename, packagename, StringComparison.CurrentCultureIgnoreCase)));
                NLogger.Debug($"{packagename} is ignored.");
            }

            InstallList = Utils.Remove(InstallList, IgnoredList);

            InstallList.Sort();
        }
    }
}
