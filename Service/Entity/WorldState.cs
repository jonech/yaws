using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Service.Entity
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

        [JsonProperty("sortie")]
        public Sortie Sortie { get; set; }

        [JsonProperty("syndicateMissions")]
        public List<SyndicateMission> SyndicateMissions { get; set; }

        [JsonProperty("fissures")]
        public List<Fissure> Fissures { get; set; }

        [JsonProperty("invasions")]
        public List<Invasion> Invasions { get; set; }

        [JsonProperty("nightwave")]
        public Nightwave Nightwave { get; set; }

    }
}
