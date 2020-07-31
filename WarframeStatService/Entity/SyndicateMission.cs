using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class SyndicateMission : Expirable
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }

        [JsonProperty("startString")]
        public string StartString { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("eta")]
        public string ETA { get; set; }

        [JsonProperty("syndicate")]
        public string Syndicate { get; set; }

        [JsonProperty("nodes")]
        public List<string> Nodes { get; set; }

        [JsonProperty("jobs")]
        public List<Job> Jobs { get; set; }
    }
}
