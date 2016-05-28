using System;
using PIN.Core.Managers;
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
    }
}
