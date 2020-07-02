using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entity
{
    public class Reward
    {
        [JsonProperty("items")]
        public List<string> Items { get; set; }

        [JsonProperty("countedItems")]
        public List<RewardItem> CountedItems { get; set; }

        [JsonProperty("credits")]
        public int Credits { get; set; }

        [JsonProperty("asString")]
        public string AsString { get; set; }

        [JsonProperty("itemString")]
        public string ItemString { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }
    }

    public class RewardItem
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
