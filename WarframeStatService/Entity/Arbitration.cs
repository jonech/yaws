using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class Arbitration : ExpirableStat
    {
        [JsonIgnore]
        public override string Id { get => $"{nameof(Arbitration)}{Expiry.Ticks}"; }
        public override WFStatType StatType => WFStatType.Arbitration;


        [JsonProperty("enemy")]
        public string Enemy { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("archwing")]
        public bool IsArchwing { get; set; }

        [JsonProperty("sharkwing")]
        public bool IsSharkwing { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

    }
}
