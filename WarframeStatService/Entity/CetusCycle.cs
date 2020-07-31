using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Base;

namespace WarframeStatService.Entity
{
    public class CetusCycle : ExpirableStat
    {
        public override WFStatType StatType => WFStatType.CetusCycle;


        [JsonProperty("isDay")]
        public bool IsDay { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("shortString")]
        public string ShortString { get; set; }

    }
}
