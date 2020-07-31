using Newtonsoft.Json;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class EarthCycle : Expirable
    {
        //[JsonProperty("id")]
        //public override string Id { get; set; }

        [JsonProperty("isDay")]
        public bool IsDay { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
