using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModel
{
    public class FactionMissionViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.FactionMission;
        public override string Name { get; }
        public List<string> Nodes { get; set; }
        public bool IsActive { get; set; }

        public FactionMissionViewModel(SyndicateMission model) : base(model)
        {
            Name = model.Syndicate;
            IsActive = model.Active;
            Nodes = model.Nodes;
        }
    }
}
