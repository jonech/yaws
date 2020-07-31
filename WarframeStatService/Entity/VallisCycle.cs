using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class VallisCycle : Expirable
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }

        [JsonProperty("isWarm")]
        public bool IsWarm { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("timeLeft")]
        public string TimeLeft { get; set; }

        [JsonProperty("shortString")]
        public string ShortString { get; set; }
    }
}
