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

        private void Wc_DownloadProgressChanged(object s, DownloadProgressChangedEventArgs e)
        {
            pb.Report((double)e.ProgressPercentage / 100);
            if (e.ProgressPercentage == 100)
            {
                DownloadComplete = true;
            }
        }
    }
}
