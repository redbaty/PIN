using System;
using System.Collections.Generic;
using static PIN.Core.Language;

namespace PIN.Core.Managers
{
    enum NotificationType
    {
        Installloaded,
        Installationsucces,
        Installaltioninvalid,
        Installationprogress,
        Programdone
    }

    static class Notification
    {
        public static void NotificationStartListening(Manager manager)
        {
            manager.ProgressChanged += ManagerOnProgressChanged;
        }

        private static void ManagerOnProgressChanged(List<string> list, NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.Installaltioninvalid:
                {
                    Utils.WriteError(string.Format(CurrentLanguage.InstallationPackageInvalid,
                        Utils.JoinArray(list.ToArray())));
                }
                    break;
                case NotificationType.Installationsucces:
                {
                    Utils.WriteinColor(ConsoleColor.DarkGreen, string.Format(CurrentLanguage.InstallationSuccess));
                }
                    break;
                case NotificationType.Installloaded:
                {
                    Utils.WriteInfo(string.Format(CurrentLanguage.InstallListLoaded));
                }
                    break;
                case NotificationType.Programdone:
                {
                    Utils.WriteInfo(CurrentLanguage.ProgramDone);
                }
                    break;
                case NotificationType.Installationprogress:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null);
            }
        }
    }
}
