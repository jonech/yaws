﻿using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Core.ViewModel
{
    public class EarthCycleViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.EarthCycle;
        public override string Name => "Earth Cycle";

        public bool IsDay { get; set; }
        public string State { get; set; }
        public string TimeLeft { get; set; }

        public EarthCycleViewModel(EarthCycle model) : base(model)
        {
            Id = model.Id;
            IsDay = model.IsDay;
            State = model.State;
            TimeLeft = model.TimeLeft;
        }

    }
}