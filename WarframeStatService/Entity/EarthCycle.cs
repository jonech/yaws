using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Base;

namespace WarframeStatService.Entity
{
    public class EarthCycle : ExpirableStat
    {
        public override WFStatType StatType => WFStatType.EarthCycle;


        [JsonProperty("isDay")]
        public bool IsDay { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
