using System;
using System.Net;
using System.Threading;
using PIN.Core;
using PIN.Core.Managers;
using PIN.Core.Packages;
using Squirrel;
using static PIN.Core.Managers.Notification;

namespace PIN
{
    class Program
    {
        static void Main(string[] args)
        {
            Language.FindTranslation("pt-BR");
            Utils.WriteVersion();

            Arguments argumentsManager = new Arguments(args);
            Manager manager = new Manager(argumentsManager);
            NotificationStartListening(manager);
            manager.StartInstallation();

            Console.ReadKey();
        }

        static async void Update()
        {
            using (var mgr = new UpdateManager("C:\\Projects\\MyApp\\Releases"))
            {
                await mgr.UpdateApp();
            }
        }
    }
}
