using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Constant = WarframeStatService.Constant;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;
using System.Linq;

namespace WarframeStatService.Entity
{
    public class SyndicateMission : ExpirableStat
    {
        public override Constant.WFStatType StatType
        {
            get
            {
                if (this.Syndicate == Constant.Syndicate.Ostrons)
                    return Constant.WFStatType.CetusBounty;
                else if (this.Syndicate == Constant.Syndicate.SolarisUnited)
                    return Constant.WFStatType.VallisBounty;

                return Constant.WFStatType.FactionMission;
            }
        }


        [JsonProperty("startString")]
        public string StartString { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("eta")]
        public string ETA { get; set; }

        [JsonProperty("syndicate")]
        public string Syndicate { get; set; }

        [JsonProperty("nodes")]
        public List<string> Nodes { get; set; }

        [JsonProperty("jobs")]
        public List<Job> Jobs { get; set; }
    }
}
