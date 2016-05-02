using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using ShellProgressBar;

namespace PIN.Core.Managers
{
    class Download
    {
        public WebClient WebHandler { get; set; } = new WebClient();
        public ChildProgressBar ProgressBar { get; set; }
        public int Progress { get; set; }
        public string PackageName { get; set; }

        public static ProgressBar DownloadProgressBar { get; set; }

        private static ProgressBarOptions DefaultStyle { get; } = new ProgressBarOptions
        {
            BackgroundColor = ConsoleColor.Gray,
            ForeGroundColor = ConsoleColor.White,
            CollapseWhenFinished = true
        };

        public static void StartDownload(string url, string path, string packagename = "")
        {
            Download downloader = new Download {PackageName = Utils.FirstCharToUpper(packagename)};

            if (DownloadProgressBar == null)      
                DownloadProgressBar = new ProgressBar(1, "Downloading", DefaultStyle);      
            else
                DownloadProgressBar.UpdateMaxTicks(DownloadProgressBar.MaxTicks + 1);

            #region Events

            downloader.WebHandler.DownloadProgressChanged += delegate(object b, DownloadProgressChangedEventArgs e)
            {
                downloader.ProgressBar.CurrentTick = (int) e.BytesReceived;
            };

            downloader.WebHandler.DownloadFileCompleted += delegate(object sender, AsyncCompletedEventArgs args)
            {
                downloader.ProgressBar.Dispose();
                DownloadProgressBar.Tick();
            };

            #endregion

            downloader.ProgressBar = DownloadProgressBar.Spawn(Utils.GetDownloadSize(url), Utils.FirstCharToUpper(packagename), DefaultStyle);

            downloader.WebHandler.DownloadFileAsync(new Uri(url), path);
        }
    }
}
