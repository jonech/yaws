using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Service.Entity
{
    public class Fissure
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("startString")]
        public string StartString { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("expired")]
        public bool Expired { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("missionType")]
        public string MissionType { get; set; }

        [JsonProperty("enemy")]
        public string Enemy { get; set; }

        [JsonProperty("tier")]
        public string Tier { get; set; }

        [JsonProperty("TierNum")]
        public string TierNum { get; set; }

        [JsonProperty("eta")]
        public string ETA { get; set; }
    }
}
