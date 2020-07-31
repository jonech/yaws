using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class SentientOutpost : ExpirableStat
    {
        public override WFStatType StatType => WFStatType.SentientOutpost;

        [JsonProperty("mission")]
        public SentientOutpostMission Mission { get; set; }
    }

    public class SentientOutpostMission
    {
        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("faction")]
        public string Faction { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
