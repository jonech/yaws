using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class Event : Expirable
    {
        //[JsonProperty("id")]
        //public override string Id { get; set; }

        [JsonProperty("maximumScore")]
        public int MaximumScore { get; set; }

        [JsonProperty("currentScore")]
        public int CurrentScore { get; set; }

        [JsonProperty("smallInterval")]
        public int SmallInterval { get; set; }

        [JsonProperty("largeInterval")]
        public int LargeInterval { get; set; }

        [JsonProperty("faction")]
        public string Faction { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tooltip")]
        public string Tooltip { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("concurrentNodes")]
        public List<string> ConcurrentNodes { get; set; }

        [JsonProperty("victimNode")]
        public string VictimNode { get; set; }

        [JsonProperty("scoreLocTag")]
        public string ScoreLocTag { get; set; }

        [JsonProperty("rewards")]
        public List<Reward> Rewards { get; set; }

        [JsonProperty("health")]
        public int Health { get; set; }

        [JsonProperty("affiliatedWith")]
        public string AffiliatedWith { get; set; }

        [JsonProperty("jobs")]
        public List<Job> Jobs { get; set; }

        [JsonProperty("interimSteps")]
        public List<InterimStep> InterimSteps { get; set; }

        [JsonProperty("progressTotal")]
        public int ProgressTotal { get; set; }

        [JsonProperty("showTotalAtEndOfMission")]
        public bool ShowTotalAtEndOfMission { get; set; }

        [JsonProperty("isPersonal")]
        public bool IsPersonal { get; set; }

        [JsonProperty("isCommunity")]
        public bool IsCommunity { get; set; }

        [JsonProperty("regionDrops")]
        public List<string> RegionDrops { get; set; }

        [JsonProperty("archwingDrops")]
        public List<string> ArchwingDrops { get; set; }

        [JsonProperty("asString")]
        public string AsString { get; set; }

        [JsonProperty("completionBonuses")]
        public List<int> CompletionBonuses { get; set; }

        [JsonProperty("scoreVar")]
        public string ScoreVar { get; set; }

        [JsonProperty("altExpiry")]
        public DateTime AltExpiry { get; set; }

        [JsonProperty("altActivation")]
        public DateTime AltActivation { get; set; }
    }

    public class InterimStep
    {
        [JsonProperty("goal")]
        public string Goal { get; set; }

        [JsonProperty("reward")]
        public Reward Reward { get; set; }
    }
}
