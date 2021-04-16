using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;

using hShopCLI.Parse;

namespace hShopCLI
{
    class hShop
    {
        //urls
        private const string APIbase = "https://hshop.erista.me/api/";
        //private const string index = "hindex";

        private RestClient rc;

        public hShop()
        {
            rc = new RestClient(APIbase);
            //Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.72 Safari/537.36
            //rc.UserAgent = $"hShopCLI/{Program.version} ({Environment.OSVersion}) .NET/5 - L7Y Media";
            rc.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.72 Safari/537.36";
        }

        public async Task<HShopIndex> GetIndex()
        {
            var rq = new RestRequest("index", Method.GET);
            var response = await rc.ExecuteAsync(rq);
            return JsonConvert.DeserializeObject<HShopIndex>(response.Content); 
        }

        public async Task<List<HShopSearch>> Search(string search)
        {
            var rq = new RestRequest("title/search?query=" + search, Method.GET);
            //rq.AddUrlSegment("query", search);
            var response = await rc.ExecuteAsync(rq);
            System.IO.File.WriteAllText("tempdebug.json", response.Content);
            return JsonConvert.DeserializeObject<List<HShopSearch>>(response.Content);
        }

        public async Task<string> RequestDownload(string id)
        {
            var downloadClient = new RestClient($"https://download4.erista.me/content/request?id={id}");
            var downloadRequest = new RestRequest(Method.GET);
            var downloadInfoRaw = await downloadClient.ExecuteAsync(downloadRequest);
            var downloadInfo = JsonConvert.DeserializeObject<HShopDownloadReq>(downloadInfoRaw.Content);
            return $"https://download4.erista.me/content/{id}?token={downloadInfo.Token}";
        }

        public async Task SearchByTitleId(string id)
        {

        }
    }
}
