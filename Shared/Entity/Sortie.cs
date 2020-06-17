﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entity
{
    public class Sortie
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("rewardPool")]
        public string RewardPool { get; set; }

        [JsonProperty("boss")]
        public string Boss { get; set; }

        [JsonProperty("faction")]
        public string Faction { get; set; }

        [JsonProperty("expired")]
        public bool Expired { get; set; }

        [JsonProperty("eta")]
        public string ETA { get; set; }

        [JsonProperty("variants")]
        public List<SortieVariant> Variants { get; set; }
    }

    public class SortieVariant
    {
        [JsonProperty("boss")]
        public string Boss { get; set; }

        [JsonProperty("planet")]
        public string Planet { get; set; }

        [JsonProperty("missionType")]
        public string MissionType { get; set; }

        [JsonProperty("modifier")]
        public string Modifier { get; set; }

        [JsonProperty("modifierDescription")]
        public string ModifierDescription { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }
    }
}
