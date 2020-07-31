using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class Job : IStat
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonIgnore]
        public WFStatType StatType => WFStatType.Job;


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


        [JsonIgnore]
        public int TotalStanding { get => StandingStages == null ? 0 : StandingStages.Sum(); }

        [JsonIgnore]
        public int MinEnemyLevel { get => EnemyLevels == null ? 0 : EnemyLevels.Min(); }

        [JsonIgnore]
        public int MaxEnemyLevel { get => EnemyLevels == null ? 0 : EnemyLevels.Max(); }
    }
}
