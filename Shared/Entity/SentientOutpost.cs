using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.Entity
{
    public class SentientOutpost
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("mission")]
        public SentientOutpostMission Mission { get; set; }
    }

    public class SentientOutpostMission
    {
        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("faction")]
        public string Faction { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
