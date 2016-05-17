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
        public PowerShell Powershell { get; set; }
        public Version Version { get; set; }
        public Paths Path { get; set; }

        public string FileType { get; set; }
        public string PowershellContent { get; set; }
        public string PackageFileContent { get; set; }

        public ChocolateyInfo(string packageName)
        {
            Path = new Paths
            {
                PowerShellFile = @"tools\chocolateyInstall.ps1",
                CompressedFile = $"{packageName}.upackage",
                PackageFile = $"{packageName}.nuspec"
            };

            DownloadBasicPackage(packageName);

            Package = JsonConvert.DeserializeObject<PackageInformation>(PackageFileContent);
            Version = new Version(Package.package.metadata.version);
            Powershell = new PowerShell(PowershellContent, Version);
            FileType = Powershell.FileType;

            File.Delete(Path.CompressedFile);
        }

        public void DownloadBasicPackage(string pap)
        {
            #region Download file

            try
            {
                new WebClient().DownloadFile($"http://chocolatey.org/api/v2/package/{pap}/", Path.CompressedFile);
            }
            catch (WebException x)
            {
                if (x.Status == WebExceptionStatus.ProtocolError)
                {
                    throw new Exception(
                        "Can't find the package on chocolatey servers. ( Make sure the packaname is exacly the same as in the website )");
                }

                throw new Exception($"Error while downloading {pap} - {x} @ ChocolateyInfo");
            }

            #endregion
            #region Read the files

            using (StreamReader outputStream = new StreamReader(Path.CompressedFile))
            {
                using (ZipFile zip = ZipFile.Read(outputStream.BaseStream))
                {
                    ZipEntry e = zip[Path.PackageFile];
                    using (StreamReader s = new StreamReader(e.OpenReader()))
                    {
                        PackageFileContent = Utils.Converter.XmlToJson(s.ReadToEnd().Replace("-xmlns", "xmlns"));
                    }

                    e = zip[Path.PowerShellFile];
                    using (StreamReader s = new StreamReader(e.OpenReader()))
                    {
                        PowershellContent = s.ReadToEnd();
                    }
                }
            }

            #endregion
        }
    }
}