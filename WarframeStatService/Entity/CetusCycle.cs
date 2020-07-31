using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity
{
    public class CetusCycle : Expirable
    {
        //[JsonProperty("id")]
        //public override string Id { get; set; }

        [JsonProperty("isDay")]
        public bool IsDay { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("shortString")]
        public string ShortString { get; set; }

    }
}
