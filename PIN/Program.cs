﻿using System;
using PIN.Core.Managers;
using PIN.Core.Misc;
using static PIN.Core.Managers.Notification;

namespace PIN
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager(args);
            NotificationStartListening(manager);

            Utils.WriteInfo("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
