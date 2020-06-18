using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entity
{
    public class Nightwave : IExpirable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("startString")]
        public string StartString { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("season")]
        public int Season { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("phase")]
        public int Phase { get; set; }

        [JsonProperty("activeChallenges")]
        public List<NightwaveChallenge> ActiveChallenges { get; set; }
    }

    public class NightwaveChallenge : IExpirable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("isDaily")]
        public bool IsDaily { get; set; }

        [JsonProperty("isElite")]
        public bool IsElite { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }
    }
}
