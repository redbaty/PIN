using System;
using System.IO;
using System.Net;
using Ionic.Zip;
using Newtonsoft.Json;
using PIN.Core.jModels;
// ReSharper disable InconsistentNaming

namespace PIN.Core.Packages
{
    class ChocolateyInfo
    {
        public PackageInformation Package { get; set; }

        public string FileType { get; set; }
        private string PackageName { get; set; }
        public PowerShell Powershell { get; set; }
        public Version Version { get; set; }
        public Paths paths { get; set; }

        internal class PowerShell
        {
            public string URL86 { get; set; }
            public string URL64 { get; set; }
            public string FileType { get; set; }
            private string Source { get; }
            public string Argument { get; set; }

            public PowerShell(string source, Version version)
            {
                Source = source;

                if (!source.Contains("$fileType") && source.Contains("msi"))
                    FileType = "msi";

                if (source.Contains("$fileType"))          
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
            paths = new Paths
            {
                PowerShellFile = @"tools\chocolateyInstall.ps1",
                CompressedFile = $"{packageName}.upackage",
                PackageFile = $"{packageName}.nuspec"
            };

            DownloadBasicPackage(packageName);

            PackageName = packageName;        
            Package = JsonConvert.DeserializeObject<PackageInformation>(Utils.Converter.XmlToJson(File.ReadAllText(packageName + ".nuspec")).Replace("-xmlns", "xmlns"));
            Version = new Version(Package.package.metadata.version);
            Powershell = new PowerShell(File.ReadAllText(@"tools\chocolateyInstall.ps1"), Version);

            File.Delete(@"tools\chocolateyInstall.ps1");
            File.Delete(paths.CompressedFile);
            File.Delete(paths.PackageFile);
            if (Directory.Exists("tools")) Directory.Delete("tools");

            if (!Directory.Exists("Downloads")) Directory.CreateDirectory("Downloads");
        }

        public void DownloadBasicPackage(string pap)
        {
            Paths cPaths = paths;

            var webHandler = new WebClient();
            webHandler.DownloadFile($"http://chocolatey.org/api/v2/package/{pap}/", cPaths.CompressedFile);

            using (StreamReader outputStream = new StreamReader(cPaths.CompressedFile))
            {
                using (ZipFile zip = ZipFile.Read(outputStream.BaseStream))
                {
                    ZipEntry e = zip[cPaths.PackageFile];
                    e.Extract(ExtractExistingFileAction.OverwriteSilently);
                    e = zip[cPaths.PowerShellFile];
                    e.Extract(ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}