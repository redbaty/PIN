using System;
using System.IO;
using NLog;
using PIN.Core.Managers;

namespace PIN.Core.Packages
{
    class Chocolatey
    {
        public ChocolateyInfo ChocolateyInfo { get; set; }
        public IAP Package { get; set; }
        private Paths CPaths { get; } = new Paths();
        private Logger NLogger = LogManager.GetCurrentClassLogger();

        public string PackageName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chocolatey"/> class.
        /// </summary>
        /// <param name="pap">The package name to download.</param>
        public Chocolatey(string pap)
        {
            if (pap.Contains("."))
            {
                NLogger.Info("Expected chocolatey package name, but got a file instead.");
                throw new Exception("Expected chocolatey package name, but got a file instead.");
            }

            Directory.CreateDirectory($"Downloads\\{pap}");

            PackageName = pap;
            CPaths.PowerShellFile = @"tools\chocolateyInstall.ps1";
            CPaths.CompressedFile = $"{pap}.upackage";
            CPaths.PackageFile = $"{pap}.nuspec";
            ChocolateyInfo = new ChocolateyInfo(PackageName);
        }

        public void StartDownload(string path = "")
        {
            var downloadDirectory = string.IsNullOrEmpty(path) ? $"Downloads\\{PackageName}\\" : path;
            var fullDownloadDirectory = Directory.GetCurrentDirectory() + $"\\{downloadDirectory}";
            var downloadedPackage = new IAP(PackageName, ChocolateyInfo.Version.ToString(), true,
                ChocolateyInfo.Powershell.Argument, $"{PackageName}.{ChocolateyInfo.FileType}",
                $"{PackageName}64.{ChocolateyInfo.FileType}", 0, fullDownloadDirectory);
            downloadedPackage.Save($"{downloadDirectory}{PackageName}.iap");

            try
            {
                Download.StartDownload(ChocolateyInfo.Powershell.URL86,
                    $"{downloadDirectory}{PackageName}.{ChocolateyInfo.FileType}", PackageName);
                Download.StartDownload(ChocolateyInfo.Powershell.URL64,
                    $"{downloadDirectory}{PackageName}64.{ChocolateyInfo.FileType}", $"{PackageName} x64");
            }
            catch (Exception e)
            {
                Download.DownloadProgressBar.Tick();
                NLogger.Error($"{PackageName} failed to download. {e}");
            }
        }
    }

    class Paths
    {
        public string PowerShellFile { get; set; }
        public string CompressedFile { get; set; }
        public string PackageFile { get; set; }
    }
}