using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Core.ViewModel
{
    public class VallisCycleViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.VallisCycle;
        public override string Name => "Vallis Cycle";

        public bool IsWarm { get; set; }
        public string State { get; set; }
        public string TimeLeft { get; set; }

        public VallisCycleViewModel(VallisCycle model) : base(model)
        {
            Id = model.Id;
            IsWarm = model.IsWarm;
            State = model.State;
            TimeLeft = model.TimeLeft;
        }
    }
}
