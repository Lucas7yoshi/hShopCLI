using Newtonsoft.Json;
namespace hShopCLI.Parse
{
    public partial class HShopDownloadReq
    {
        [JsonProperty("entry_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? EntryId { get; set; }

        [JsonProperty("expiry_date", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpiryDate { get; set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }
}
