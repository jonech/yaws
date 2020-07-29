using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Service.Entity
{
    public class Arbitration : IExpirable
    {
        [JsonIgnore]
        public string Id { get => $"{nameof(Arbitration)}{Expiry.Ticks}"; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

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
