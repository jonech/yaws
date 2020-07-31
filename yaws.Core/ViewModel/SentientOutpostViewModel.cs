using WarframeStatService.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Core.ViewModel
{
    public class SentientOutpostViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.SentientOutpost;
        public override string Name => "Sentient Outpost";

        public string Node { get; set; }
        public string Faction { get; set; }
        public string Type { get; set; }

        public SentientOutpostViewModel(SentientOutpost model) : base(model)
        {
            if (model.Mission != null)
            {
                Node = model.Mission.Node;
                Faction = model.Mission.Faction;
                Type = model.Mission.Type;
            }
        }
    }
}
