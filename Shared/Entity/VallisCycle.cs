﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.Entity
{
    public class VallisCycle
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("isWarm")]
        public bool IsWarm { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("timeLeft")]
        public string TimeLeft { get; set; }
    }
}
