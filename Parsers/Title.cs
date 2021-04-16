using Newtonsoft.Json;

namespace hShopCLI.Parse
{
    public partial class HShopSearch
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("title_id", NullValueHandling = NullValueHandling.Ignore)]
        public string TitleId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long? Size { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty("subcategory", NullValueHandling = NullValueHandling.Ignore)]
        public string Subcategory { get; set; }

        [JsonProperty("added_date", NullValueHandling = NullValueHandling.Ignore)]
        public string AddedDate { get; set; }

        [JsonProperty("updated_date", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedDate { get; set; }
    }
}
