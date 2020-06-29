using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Service.Entity
{
    public class Invasion : IActivatable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("startString")]
        public string StartString { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("attackingFaction")]
        public string AttackingFaction { get; set; }

        [JsonProperty("attackerReward")]
        public InvasionReward AttackerReward { get; set; }

        [JsonProperty("defendingFaction")]
        public string DefendingFaction { get; set; }

        [JsonProperty("defenderReward")]
        public InvasionReward DefenderReward { get; set; }

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

    public class InvasionReward
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
