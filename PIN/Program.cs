using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLineParser.Arguments;
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
            Manager manager = new Manager(args);
            NotificationStartListening(manager);
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
