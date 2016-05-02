using System.Collections.Generic;
using System.IO;
using System.Net;
using Ionic.Zip;
using PIN.Core.Managers;

namespace PIN.Core.Packages
{
    class Chocolatey
    {
        public IAP Package { get; set; }
        private WebClient WebHandler { get; } = new WebClient();
        private Paths CPaths { get; } = new Paths();
        public string PackageName { get; set; }
        public ChocolateyInfo ChocolateyInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chocolatey"/> class.
        /// </summary>
        /// <param name="pap">A package name (for download) or package path (for update).</param>
        public Chocolatey(string pap)
        {
            if (pap.Contains(".")) return;
            PackageName = pap;

            CPaths.PowerShellFile = @"tools\chocolateyInstall.ps1";
            CPaths.CompressedFile = $"{pap}.upackage";
            CPaths.PackageFile = $"{pap}.nuspec";
            ChocolateyInfo = new ChocolateyInfo(PackageName);
        }

        public void StartDownload(string path = "")
        {
            var downloadDirectory = string.IsNullOrEmpty(path) ? $"Downloads\\{PackageName}\\" : path; Directory.CreateDirectory(downloadDirectory);
            var fullDownloadDirectory = Directory.GetCurrentDirectory() + $"\\{downloadDirectory}";
            var downloadedPackage = new IAP(PackageName, ChocolateyInfo.Version.ToString(), $"http://chocolatey.org/api/v2/package/{PackageName}/", ChocolateyInfo.Powershell.Argument, $"{PackageName}.exe", $"{PackageName}64.exe", 0, fullDownloadDirectory);
            downloadedPackage.Save($"{downloadDirectory}{PackageName}.iap");

            Download.StartDownload(ChocolateyInfo.Powershell.URL86, $"{downloadDirectory}{PackageName}.msi", PackageName);
            Download.StartDownload(ChocolateyInfo.Powershell.URL64, $"{downloadDirectory}{PackageName}64.msi", $"{PackageName} x64");       
        }
    }

    class Paths
    {
        public string PowerShellFile { get; set; }
        public string CompressedFile { get; set; }
        public string PackageFile { get; set; }
    }
}