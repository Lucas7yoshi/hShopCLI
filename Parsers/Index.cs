using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace hShopCLI.Parse
{
    public partial class HShopIndex
    {
        [JsonProperty("entries", NullValueHandling = NullValueHandling.Ignore)]
        public Entries Entries { get; set; }

        [JsonProperty("total_size", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalSize { get; set; }

        [JsonProperty("updated_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UpdatedDate { get; set; }

        [JsonProperty("total_title_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalTitleCount { get; set; }
    }

    public partial class Entries
    {
        [JsonProperty("games", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Games { get; set; }

        [JsonProperty("updates", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Updates { get; set; }

        [JsonProperty("dlc", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Dlc { get; set; }

        [JsonProperty("vc", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Vc { get; set; }

        [JsonProperty("injects", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Injects { get; set; }

        [JsonProperty("dsiware", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Dsiware { get; set; }

        [JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Extras { get; set; }

        [JsonProperty("themes", NullValueHandling = NullValueHandling.Ignore)]
        public Dlc Themes { get; set; }
    }

    public partial class Dlc
    {
        [JsonProperty("subcategories", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Dlc> Subcategories { get; set; }

        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("content_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? ContentCount { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long? Size { get; set; }
    }
}
