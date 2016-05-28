using System;
using System.IO;
using NLog;
using PIN.Core.Misc;

namespace PIN.Core.Packages
{
    class Chocolatey
    {
        public ChocolateyInfo ChocolateyInfo { get; set; }
        public Paths CPaths { get; } = new Paths();
        public string PackageName { get; set; }


        private readonly Logger _nLogger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Initializes a new instance of the <see cref="Chocolatey"/> class.
        /// </summary>
        /// <param name="pap">The package name to download.</param>
        public Chocolatey(string pap)
        {
            if (pap.Contains("."))
            {
                _nLogger.Info("Expected chocolatey package name, but got a file instead.");
                throw new Exception("Expected chocolatey package name, but got a file instead.");
            }

            Directory.CreateDirectory($"Downloads\\{pap}");

            PackageName = pap;
            CPaths.PowerShellFile = @"tools\chocolateyInstall.ps1";
            CPaths.CompressedFile = $"{pap}.upackage";
            CPaths.PackageFile = $"{pap}.nuspec";
            ChocolateyInfo = new ChocolateyInfo(PackageName);
        }

        /// <summary>
        /// Downloads the specified packaged.
        /// </summary>
        /// <param name="path">The path to download to.</param>
        public void Download(string path = "")
        {
            var downloadDirectory = string.IsNullOrEmpty(path) ? $"Downloads\\{PackageName}\\" : path;
            var fullDownloadDirectory = Directory.GetCurrentDirectory() + $"\\{downloadDirectory}";
            new Package(PackageName, ChocolateyInfo.Version.ToString(), true, ChocolateyInfo.Powershell.Argument,
                $"{PackageName}.{ChocolateyInfo.FileType}", $"{PackageName}64.{ChocolateyInfo.FileType}", 0,
                fullDownloadDirectory).Save($"{downloadDirectory}{PackageName}{Package.FileType}");

            try
            {
                ChocolateyDownloader.StartDownload(ChocolateyInfo.Powershell.URL86,
                    $"{downloadDirectory}{PackageName}.{ChocolateyInfo.FileType}", PackageName);
                ChocolateyDownloader.StartDownload(ChocolateyInfo.Powershell.URL64,
                    $"{downloadDirectory}{PackageName}64.{ChocolateyInfo.FileType}", $"{PackageName} x64");
            }
            catch (Exception e)
            {
                ChocolateyDownloader.DownloadProgressBar.Tick();
                _nLogger.Error($"{PackageName} failed to download. {e}");
            }
        }

    }
}