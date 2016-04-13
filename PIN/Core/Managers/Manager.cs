using System;
using System.Collections.Generic;
using System.Linq;
using Konsole;
using PIN.Core.jModels;
using PIN.Core.Packages;

namespace PIN.Core.Managers
{
    class Manager
    {

        public List<IAP> IgnoredList { get; set; }
        public List<IAP> InstallList { get; set; }

        public ProgressBar ProgressBar { get; set; }

        public string[] PackagesNames
        {
            get { return InstallList.Select(iap => iap.Packagename).ToArray(); }
        }

        public event ManagerProgress ProgressChanged;
        internal delegate void ManagerProgress(List<string> data, NotificationType notificationType);

        public Manager(Arguments arguments)
        {
            IgnoredList = new List<IAP>();

            InstallList = new Scanner().Scan();
            InstallList = ValidateInstallations();
            InstallList.Sort();

            ProgressChanged += OnProgressChanged;

            if (arguments.Value.ContainsKey("ignore"))
            {
                foreach (var packagename in arguments.Value.SelectMany(aa => aa.Value))
                {
                    if (IgnoredList != null) IgnoredList.Add(InstallList.Find(x => String.Equals(x.Packagename, packagename, StringComparison.CurrentCultureIgnoreCase)));
                }
                
                InstallList = Utils.Remove(InstallList, IgnoredList);
            }
        }

        public void InvokeTest()
        {
            System.Diagnostics.Debug.Assert(ProgressChanged != null, "ProgressChanged != null");
            ProgressChanged(null, NotificationType.INSTALLLOADED);
        }

        public void Debug()
        {
            Console.WriteLine("Installation List");
            foreach (IAP iap in InstallList)
            {
                Utils.WriteinColor(ConsoleColor.Gray, iap.Packagename);
            }

            Console.WriteLine("\nIgnore List");
            foreach (IAP iap in IgnoredList)
            {
                Utils.WriteinColor(ConsoleColor.Gray, iap.Packagename);
            }
        }      

        /// <summary>
        /// Starts all the installations.
        /// </summary>
        /// <param name="args">if set to <c>true</c> then the application will be executed with all the designed arguments.</param>
        public void StartInstallation(bool args = true)
        {
            ProgressBar.FORMAT = Translation.InstallationProgressBarFormat;
            ProgressBar = new ProgressBar(InstallList.Count - 1);

            for (int index = 0; index < InstallList.Count; index++)
            {
                IAP iap = InstallList[index];
                ProgressBar.Refresh(index, iap.Packagename);
                if (ProgressChanged != null) ProgressChanged(new List<string> {iap.Packagename}, NotificationType.INSTALLATIONPROGRESS);
                iap.Install(args);
            }

            if (ProgressChanged != null) ProgressChanged(null, NotificationType.INSTALLATIONSUCCES);
        }

        /// <summary>
        /// Validates the installations.
        /// </summary>
        /// <returns></returns>
        List<IAP> ValidateInstallations()
        {
            List<IAP> errors = InstallList.Where(iap => !iap.ValidateInstall()).ToList();
            foreach (IAP error in errors) { InstallList.Remove(error); }
            if (errors.Count > 0) { Utils.WriteError(string.Format("Pacotes invalidos : " + Utils.JoinArray(errors.Select(invalid => invalid.Packagename).ToArray()))); }
            return InstallList;
        }

        protected virtual void OnProgressChanged(List<string> data, NotificationType notificationtype)
        {

        }
    }

    
}
