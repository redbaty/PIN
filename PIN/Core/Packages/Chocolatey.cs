using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NLog;

namespace PIN.Core.Packages
{
    class Chocolatey
    {
        public ChocolateyInfo ChocolateyInfo { get; set; }       
        public IAP Package { get; set; }     
        public string PackageName { get; set; }
        private Paths CPaths { get; } = new Paths();


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
                CDownloader.StartDownload(ChocolateyInfo.Powershell.URL86,
                    $"{downloadDirectory}{PackageName}.{ChocolateyInfo.FileType}", PackageName);
                CDownloader.StartDownload(ChocolateyInfo.Powershell.URL64,
                    $"{downloadDirectory}{PackageName}64.{ChocolateyInfo.FileType}", $"{PackageName} x64");
            }
            catch (Exception e)
            {
                CDownloader.DownloadProgressBar.Tick();
                _nLogger.Error($"{PackageName} failed to download. {e}");
            }
        }

        public static void Download(List<string> packages)
        {
            foreach (var package in packages)
            {
                try
                {
                    var download = new Chocolatey(package);
                    download.StartDownload();
                }
                catch (Exception ex)
                {
                    Utils.WriteError($"An error occurred while downloading {package} - {ex}");
                }
            }

            while (CDownloader.DownloadProgressBar.CurrentTick < CDownloader.DownloadProgressBar.MaxTicks)
            {
                Thread.Sleep(20);
            }
            CDownloader.DownloadProgressBar.Dispose();
        }
    }
}