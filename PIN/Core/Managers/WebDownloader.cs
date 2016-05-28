using System;
using System.Diagnostics;
using System.Net;

namespace PIN.Core.Managers
{
    class WebDownloader : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest webRequest = base.GetWebRequest(uri);
            Debug.Assert(webRequest != null, "webRequest != null");
            webRequest.Timeout = 9999;
            return webRequest;
        }
    }
}

