using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class Invasion : ActivatableStat
    {
        public override WFStatType StatType => WFStatType.Invasion;

        [JsonProperty("startString")]
        public string StartString { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("attackingFaction")]
        public string AttackingFaction { get; set; }

        [JsonProperty("attackerReward")]
        public Reward AttackerReward { get; set; }

        [JsonProperty("defendingFaction")]
        public string DefendingFaction { get; set; }

        [JsonProperty("defenderReward")]
        public Reward DefenderReward { get; set; }

        [JsonProperty("vsInfestation")]
        public bool VSInfestation { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("requiredRuns")]
        public int RequiredRuns { get; set; }

        [JsonProperty("completion")]
        public float Completion { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("eta")]
        public string ETA { get; set; }

        [JsonProperty("rewardTypes")]
        public List<string> RewardTypes { get; set; }
    }
}
