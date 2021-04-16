using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace hShopCLI
{
    class Downloader
    {
        private WebClient wc;
        private Uri url;
        private ProgressBar pb;

        private bool DownloadComplete;

        public Downloader(string urlIn)
        {
            wc = new();
            wc.DownloadProgressChanged += Wc_DownloadProgressChanged;

            url = new Uri(urlIn);
        }


        public Downloader(Uri uriIn)
        {
            wc = new();
            url = uriIn;
        }

        public async Task BeginDownload(string destination)
        {
            pb = new();

            wc.DownloadFileAsync(url, destination);
            while (!DownloadComplete)
            {
                await Task.Delay(250);
            }
            pb.Dispose();
        }

        private DateTime lastUpdate;
        long lastBytes = 0;

        private void Wc_DownloadProgressChanged(object s, DownloadProgressChangedEventArgs e)
        {
            if (lastBytes == 0)
            {
                lastUpdate = DateTime.Now;
                lastBytes = e.BytesReceived;
            }

            var now = DateTime.Now;
            var timeSpanSec = (now - lastUpdate).Seconds;
            
            var bytesChange = e.BytesReceived - lastBytes;
            if (timeSpanSec == 0)
                timeSpanSec = 1;
            var bytesPerSecond = bytesChange / timeSpanSec;

            lastBytes = e.BytesReceived;
            lastUpdate = now;


            pb.Report((double)e.ProgressPercentage / 100);
            if (e.ProgressPercentage == 100)
            {
                DownloadComplete = true;
            }
        }
    }
}
