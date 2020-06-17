using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.Entity
{
    public class WorldState
    {
        [JsonProperty("arbitration")]
        public Arbitration Arbitration { get; set; }

        [JsonProperty("cetusCycle")]
        public CetusCycle CetusCycle { get; set; }

        [JsonProperty("earthCycle")]
        public EarthCycle EarthCycle { get; set; }

        [JsonProperty("sentientOutposts")]
        public SentientOutpost SentientOutpost { get; set; }

        [JsonProperty("vallisCycle")]
        public VallisCycle VallisCycle { get; set; }
    }
}
