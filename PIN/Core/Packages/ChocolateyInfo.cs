using System;
using System.IO;
using System.Net;
using Ionic.Zip;
using Newtonsoft.Json;
using PIN.Core.jModels;
using PIN.Core.Misc;

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
        }

        /// <summary>
        /// Downloads the basic package which contains informations about the chocolatey server version of the package.
        /// </summary>
        /// <param name="pap">The package name.</param>
        /// <exception cref="Exception">
        /// Can't find the package on chocolatey servers. ( Make sure the packaname is exacly the same as in the website )
        /// or
        /// </exception>
        public void DownloadBasicPackage(string pap)
        {
            #region Download file

            try
            {
                WebClient wc = new WebClient();
                using (MemoryStream stream = new MemoryStream(wc.DownloadData($"http://chocolatey.org/api/v2/package/{pap}/")))
                {
                    using (var outputStream = new StreamReader(stream))
                    {
                        using (ZipFile zip = ZipFile.Read(outputStream.BaseStream))
                        {
                            ZipEntry e = zip[Path.PackageFile];
                            using (var s = new StreamReader(e.OpenReader()))
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
                }

                new WebClient().DownloadFile($"http://chocolatey.org/api/v2/package/{pap}/", Path.CompressedFile);
            }
            catch (WebException x)
            {
                if (x.Status == WebExceptionStatus.ProtocolError)
                    throw new Exception("Can't find the package on chocolatey servers. ( Make sure the packaname is exacly the same as in the website )");

                throw new Exception($"Error while downloading {pap} - {x} @ ChocolateyInfo");
            }

            #endregion
        }
    }
}