using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Service.Entity
{
    public class SyndicateMission : IExpirable
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
