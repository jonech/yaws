using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;

namespace yaws.Core.ViewModel
{
    public class CetusCycleViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.CetusCycle;
        public override string Name => "Cetus Cycle";

        public bool IsDay { get; set; }

        public string State { get; set; }

        public string TimeLeft { get; set; }


        public CetusCycleViewModel(CetusCycle cetusCycle) : base(cetusCycle)
        {
            Id = cetusCycle.Id;
            IsDay = cetusCycle.IsDay;
            State = cetusCycle.State;
            TimeLeft = cetusCycle.TimeLeft;
        }
    }
}
