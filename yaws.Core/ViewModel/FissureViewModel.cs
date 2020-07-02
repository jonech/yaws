using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Core.ViewModel
{
    public class FissureViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.Fissure;

        public override string Name => "Fissure";

        public bool Expired { get; set; }
        public string Node { get; set; }
        public string MissionType { get; set; }
        public string Enemy { get; set; }
        public string Tier { get; set; }
        public int TierNum { get; set; }

        public FissureViewModel(Fissure model) : base(model)
        {
            Expired = model.Expired;
            Node = model.Node;
            MissionType = model.MissionType;
            Enemy = model.Enemy;
            Tier = model.Tier;
            TierNum = model.TierNum;
        }
    }
}
