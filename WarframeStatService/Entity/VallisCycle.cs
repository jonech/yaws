using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class VallisCycle : ExpirableStat
    {
        public override WFStatType StatType => WFStatType.VallisCycle;


        [JsonProperty("isWarm")]
        public bool IsWarm { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("shortString")]
        public string ShortString { get; set; }
    }
}
