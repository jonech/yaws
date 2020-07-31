using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class Fissure : Expirable
    {
        //[JsonProperty("id")]
        //public override string Id { get; set; }

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
        public int TierNum { get; set; }

        [JsonProperty("eta")]
        public string ETA { get; set; }
    }
}
