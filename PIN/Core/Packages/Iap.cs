﻿using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PIN.Core.Packages
{
    class IAP : IComparable<IAP>
    {
        [JsonIgnore]
        public string BasePath { get; set; }

        [JsonIgnore]
        public string FileName { get; set; }

        public string Version { get; set; }

        public string Packagename { get; set; }     
        public string Arguments { get; set; }
        public string Executable { get; set; }
        public string Executablex64 { get; set; }     

        public int InstallationIndex { get; set; }
        public bool ChocolateySupport { get; set; }


        public IAP()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IAP"/> class.
        /// </summary>
        /// <param name="path">The initial .</param>
        public IAP(string path)
        {          
            if (File.Exists(path))
            {
                IAP x = JsonConvert.DeserializeObject<IAP>(File.ReadAllText(path));
                Packagename = x.Packagename;
                Version = x.Version;
                ChocolateySupport = x.ChocolateySupport;
                Arguments = x.Arguments;
                BasePath = Path.GetDirectoryName(path) + @"\";
                Executable = x.Executable;
                Executablex64 = x.Executablex64;
                InstallationIndex = x.InstallationIndex;
                FileName = Path.GetFileName(path);

                Save(path);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IAP"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="version">The local version.</param>
        /// <param name="supportChocolatey">The package support chocolatey updates/installation.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="exec">The executable path.</param>
        /// <param name="execx64">The x64 executable path.</param>
        /// <param name="iIndex">Installation priority.</param>
        /// <param name="bPath">The base path.</param>
        public IAP(string name, string version, bool supportChocolatey, string arguments, string exec, string execx64, int iIndex, string bPath = "")
        {
            Packagename = name;
            Version = version;
            ChocolateySupport = supportChocolatey;
            Arguments = arguments;
            BasePath = bPath;
            Executable = exec;
            Executablex64 = execx64;
            InstallationIndex = iIndex;
        }

        /// <summary>
        /// Installs using the specified arguments.
        /// </summary>
        /// <param name="useArguments">if set to <c>true</c> [use arguments].</param>
        public void Install(bool useArguments = true)
        {
            try
            {
                Process installationProcess;
                var inst32 = Sumpath(BasePath, Executable);
                var inst64 = Sumpath(BasePath, Executablex64);
                
                if (useArguments)
                    installationProcess = Utils.InternalCheckIsWow64() ? Process.Start(inst64, Arguments) : Process.Start(inst32, Arguments);     
                else              
                    installationProcess = Utils.InternalCheckIsWow64() ? Process.Start(inst64) : Process.Start(inst32);

                installationProcess?.WaitForExit();
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao instalar o pacote {0}.", Packagename);
            }
        }

        /// <summary>
        /// Validates all the installation.
        /// </summary>
        /// <returns>return <c>true</c> if it's valid.</returns>
        public bool ValidateInstall()
        {
            var inst32 = Sumpath(BasePath, Executable);
            var inst64 = Sumpath(BasePath, Executablex64);
            if (!File.Exists(inst32)) return false;
            if (!File.Exists(inst64)) return false;

            return true;
        }

        /// <summary>
        /// Sums two Path.
        /// </summary>
        /// <param name="path">The base path.</param>
        /// <param name="add">The path to sum.</param>
        /// <returns></returns>
        public string Sumpath(string path, string add)
        {
            return $"{path}{add}";
        }

        /// <summary>
        /// Saves this instance into the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Save(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }

        /// <summary>
        /// Print out information about this instance.
        /// </summary>
        public void Debug()
        {
            Console.WriteLine("Package Name: {0}\n" +
                              "Local Version: {1}\n" +
                              "Executable Location: {2}\n" +
                              "Executable Location x64: {3}", Packagename, Version, Executable, Executablex64);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="comparePart">The compare part.</param>
        /// <returns></returns>
        public int CompareTo(IAP comparePart)
        {
            if (comparePart == null)
                return 1;

            return InstallationIndex.CompareTo(comparePart.InstallationIndex);
        }

        public void Update()
        { 
            var info = new ChocolateyInfo(Packagename);
           
            if (info.Version > new Version(Version))
            {
                if (!string.IsNullOrEmpty(info.Powershell.URL86))
                    CDownloader.StartDownload(info.Powershell.URL86, $"{BasePath}{Executable}", Packagename);

                if (!string.IsNullOrEmpty(info.Powershell.URL64))
                    CDownloader.StartDownload(info.Powershell.URL64, $"{BasePath}{Executablex64}", $"{Packagename} x64");

                Version = info.Version.ToString();
                Save(BasePath + FileName);

                Utils.WriteInfo($"{Packagename} update");
            }
            else
                Utils.WriteInfo($"{Packagename} does not need updates");
            
        }

        public static void Example(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            IAP pacakge = new IAP("Example","0.0.0.0", false, "-s", "test.exe", "test64.exe", 0, Path.GetDirectoryName(path) + @"\");  
            pacakge.Save(path);      
        }
    }
}
