using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Service.Entity
{
    public class CetusCycle : IExpirable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("isDay")]
        public bool IsDay { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("timeLeft")]
        public string TimeLeft { get; set; }

        [JsonProperty("shortString")]
        public string ShortString { get; set; }

    }
}
