using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using PIN.Core;
using PIN.Core.Forms;
using PIN.Core.jModels;
using PIN.Core.Managers;

namespace PIN
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Arguments argumentsManager = new Arguments(args);
            Manager Manager = new Manager(new Arguments(args));
            new Notification(Manager);

            Console.SetWindowSize(Console.LargestWindowWidth - 50, Console.WindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth - 50, Console.WindowHeight);
            Utils.WriteVersion();

            if (argumentsManager.Valueless.Contains("create"))
                StartCreator();
            
            Manager.StartInstallation();

            Console.ReadKey();
        }

        static void StartCreator()
        {
            iCreator x = new iCreator();
            x.Closed += delegate
            {
                Process.GetCurrentProcess().Kill();
            };
            x.ShowDialog();
        }
    }
}
