using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entity
{
    public class Job
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
