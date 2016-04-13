using System;
using System.Collections.Generic;
using PIN.Core.jModels;

namespace PIN.Core.Managers
{
    enum NotificationType
    {
        INSTALLLOADED,
        INSTALLATIONSUCCES,
        INSTALLALTIONINVALID,
        INSTALLATIONPROGRESS
    }

    class Notification
    {
        private Manager Manager { get; set; }

        public Notification(Manager manager)
        {
            manager.ProgressChanged += ManagerOnProgressChanged;
        }

        private void ManagerOnProgressChanged(List<string> list, NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.INSTALLALTIONINVALID:
                {
                    Utils.WriteError(string.Format(Translation.InstallationPackageInvalid, Utils.JoinArray(list.ToArray())));
                }
                    break;
                case NotificationType.INSTALLATIONSUCCES:
                    {
                        Utils.WriteinColor(ConsoleColor.DarkGreen , string.Format(Translation.InstallationSuccess));
                    }
                    break;
                case NotificationType.INSTALLLOADED:
                    {
                        Utils.WriteInfo(string.Format(Translation.InstallListLoaded));
                    }
                    break;
            }
        }
    }
}
