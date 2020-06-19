﻿using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModel
{
    public class SentientOutpostViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.SentientOutpost;

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

            StartTimer();
        }
    }
}
