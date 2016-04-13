using System;
using System.Net;
using Konsole;
using PIN.Core.jModels;

namespace PIN.Core.Managers
{
    class Downloader
    {
        private WebClient WebHandler { get; set; }
        private ProgressBar ProgressBar { get; set; }
        private int Progress { get; set; }


        private string PackageName { get; set; }

        public Downloader(string url, string path, string packagename = "")
        {
            PackageName = Utils.FirstCharToUpper(packagename);

            WebRequest req = WebRequest.Create(url);
            req.Method = "HEAD";
            using (WebResponse resp = req.GetResponse())
            {
                int ContentLength;
                if (int.TryParse(resp.Headers.Get("Content-Length"), out ContentLength))
                {
                    ProgressBar.FORMAT = Translation.DownloadProgressBarFormat;
                    ProgressBar = new ProgressBar(Utils.ConvertBytesToMegabytes(ContentLength));
                }
            }

            WebHandler = new WebClient();
            WebHandler.DownloadProgressChanged += WebHandlerOnDownloadProgressChanged;
            WebHandler.DownloadFileAsync(new Uri(url), path);
        }

        private void WebHandlerOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs downloadProgressChangedEventArgs)
        {
            ProgressBar.Refresh(Utils.ConvertBytesToMegabytes(downloadProgressChangedEventArgs.BytesReceived), PackageName);
            Progress++;
        }
    }
}
