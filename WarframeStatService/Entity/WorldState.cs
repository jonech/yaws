using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WarframeStatService.Constant;

namespace WarframeStatService.Entity
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


        [JsonIgnore]
        public SyndicateMission CetusBounty
        {
            get
            {
                if (SyndicateMissions != null)
                    return SyndicateMissions.FirstOrDefault(m => m.Syndicate == Syndicate.Ostrons);

                return null;
            }
        }

        [JsonIgnore]
        public SyndicateMission VallisBounty
        {
            get
            {
                if (SyndicateMissions != null)
                    return SyndicateMissions.FirstOrDefault(m => m.Syndicate == Syndicate.SolarisUnited);

                return null;
            }
        }
    }
}
