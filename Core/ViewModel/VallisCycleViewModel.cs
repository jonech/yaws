using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModel
{
    public class VallisCycleViewModel : ExpirableViewModel
    {
        public override StatType StatType => throw new NotImplementedException();
        public bool IsWarm { get; set; }
        public string State { get; set; }
        public string TimeLeft { get; set; }

        public VallisCycleViewModel(VallisCycle model) : base(model)
        {
            Id = model.Id;
            IsWarm = model.IsWarm;
            State = model.State;
            TimeLeft = model.TimeLeft;

            StartTimer();
        }
    }
}
