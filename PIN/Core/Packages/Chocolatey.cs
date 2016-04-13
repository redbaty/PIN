using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Ionic.Zip;
using Newtonsoft.Json;
using PIN.Core.jModels;
using PIN.Core.Managers;

namespace PIN.Core.Packages
{
    class Chocolatey
    {
        public IAP Package { get; set; }
        private WebClient WebHandler { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chocolatey"/> class.
        /// </summary>
        /// <param name="PAP">A package name (for download) or package path (for update).</param>
        public Chocolatey(string PAP)
        {
            WebHandler = new WebClient();
            WebHandler.DownloadFile(string.Format("http://chocolatey.org/api/v2/package/{0}/", PAP), PAP + ".upackage");

            var powershellFile = @"tools\chocolateyInstall.ps1";
            var packageFile = PAP + ".nuspec";

            using (StreamReader OutputStream = new StreamReader(PAP + ".upackage"))
            {
                using (ZipFile zip = ZipFile.Read(OutputStream.BaseStream))
                {
                    ZipEntry e = zip[packageFile];
                    e.Extract(ExtractExistingFileAction.OverwriteSilently);
                    e = zip[powershellFile];
                    e.Extract(ExtractExistingFileAction.OverwriteSilently);
                }
            }


            ChocolateyInfo c = new ChocolateyInfo(PAP);

            if (!File.Exists(PAP))
            {
                new Downloader(c.Powershell.URL86, @"Downloads\" + PAP + ".exe", PAP);
                new Downloader(c.Powershell.URL64, @"Downloads\" + PAP + "64.exe", PAP + " x64");
                IAP i = new IAP(PAP, c.Version.ToString(), string.Format("http://chocolatey.org/api/v2/package/{0}/", PAP), c.Powershell.Argument, PAP + ".exe", PAP + "64.exe", 0, Directory.GetCurrentDirectory() + @"\Downloads\");
                i.Save(@"Downloads\" + PAP + ".iap");
            }
            else
            {
                IAP package = new IAP(PAP);
                var packageVersion = new Version(package.Version);
                if (c.Version > packageVersion)
                {
                    new Downloader(c.Powershell.URL86, package.BasePath, PAP);
                    new Downloader(c.Powershell.URL64, package.BasePath, PAP + " x64");
                }
            }
        }
    }

    class ChocolateyInfo
    {
        public PackageInformation Package { get; set; }

        private string FileType { get; set; }
        private string PackageName { get; set; }
        public PowerShell Powershell { get; set; }
        private Dictionary<string, string> Files { get; set; }
        public Version Version { get; set; }
        private int Line { get; set; }

        internal class PowerShell
        {
            public string URL86 { get; set; }
            public string URL64 { get; set; }
            public string FileType { get; set; }
            private string Source { get; set; }
            public string Argument { get; set; }

            public PowerShell(string source, Version version)
            {
                Source = source;

                if (source.Contains("msi"))
                    FileType = "msi";          
                else if (source.Contains("$fileType"))          
                    FileType = Utils.GetPowershellValue("$fileType", Source);
                

                if (Source.Contains("$silentArgs"))
                    Argument = Utils.GetPowershellValue("$silentArgs", Source).Replace("\r", "");
                if (Source.Contains("$url"))
                    URL86 = GetUrl86(Source, version).Replace("https", "http");
                if (Source.Contains("$url64"))
                    URL64 = GetUrl64(Source, version).Replace("https", "http");

            }

            private string GetUrl86(string source, Version newestversion)
            {
                return Utils.GetPowershellValue("$url", source).Replace("${version}", newestversion.ToString()).Replace("${locale}", "pt-BR");
            }

            private string GetUrl64(string source, Version newestversion)
            {
                return Utils.GetPowershellValue("$url64", source).Replace("${version}", newestversion.ToString()).Replace("${locale}", "pt-BR");
            }
        }

        public ChocolateyInfo(string packageName)
        {
            PackageName = packageName;        
            Package = JsonConvert.DeserializeObject<PackageInformation>(Utils.Converter.XmlToJson(File.ReadAllText(packageName + ".nuspec")).Replace("-xmlns", "xmlns"));
            Version = new Version(Package.package.metadata.version);
            Powershell = new PowerShell(File.ReadAllText(@"tools\chocolateyInstall.ps1"), Version);

            File.Delete(@"tools\chocolateyInstall.ps1");
            File.Delete(packageName + ".nuspec");
            File.Delete(packageName + ".upackage");
            if (Directory.Exists("tools")) Directory.Delete("tools");

            if (!Directory.Exists("Downloads")) Directory.CreateDirectory("Downloads");
        }
    }
}
