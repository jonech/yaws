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
        public List<SyndicateMissionJob> Jobs { get; set; }
    }

    public class SyndicateMissionJob
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rewardPool")]
        public List<string> RewardPool { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("enemyLevels")]
        public List<int> EnemyLevels { get; set; }

        [JsonProperty("standingStages")]
        public List<int> StandingStages { get; set; }

        [JsonProperty("minMR")]
        public int MinimumMR { get; set; }


    }
}
