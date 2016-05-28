using System;
using System.Net;
using System.Threading;
using NLog;
using PIN.Core.Managers;
using PIN.Core.Misc;
using ShellProgressBar;

// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace PIN.Core.Packages
{
    class ChocolateyDownloader
    {
        public WebDownloader WebHandler { get; set; } = new WebDownloader();
        public ChildProgressBar ProgressBar { get; set; }
        public int Progress { get; set; }
        public string PackageName { get; set; }
        public static ProgressBar DownloadProgressBar { get; set; }

        private static Logger NLogger = LogManager.GetCurrentClassLogger();
        private static ProgressBarOptions DefaultStyle { get; } = new ProgressBarOptions
        {
            BackgroundColor = ConsoleColor.Gray,
            ForeGroundColor = ConsoleColor.White,
            CollapseWhenFinished = true
        };

        public static void StartDownload(string url, string path, string packagename = "")
        {
            ChocolateyDownloader downloader = new ChocolateyDownloader { PackageName = Utils.FirstCharToUpper(packagename) };

            if (DownloadProgressBar == null)
                DownloadProgressBar = new ProgressBar(1, "Downloading", DefaultStyle);
            else
                DownloadProgressBar.UpdateMaxTicks(DownloadProgressBar.MaxTicks + 1);

            #region Events
            downloader.WebHandler.DownloadProgressChanged += delegate (object b, DownloadProgressChangedEventArgs e)
            {
                downloader.ProgressBar.CurrentTick = (int)e.BytesReceived;
            };

            downloader.WebHandler.DownloadFileCompleted += delegate
            {
                downloader.ProgressBar.Dispose();
                DownloadProgressBar.Tick();
                NLogger.Info($"Sucessfully downloaded {downloader.PackageName}");
            };
            #endregion

            downloader.ProgressBar = DownloadProgressBar.Spawn(Utils.GetDownloadSize(url), Utils.FirstCharToUpper(packagename), DefaultStyle);
            downloader.WebHandler.DownloadFileAsync(new Uri(url), path);
        }

        public static void WaitAndDispose()
        {
            while (DownloadProgressBar.CurrentTick < DownloadProgressBar.MaxTicks)
            {
                Thread.Sleep(20);
            }
            DownloadProgressBar.Dispose();
        }
    }
}