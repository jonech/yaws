using Shared.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;

namespace Core.ViewModel
{
    public class CetusCycleViewModel : ViewModelBase
    {
        public override StatType Type => StatType.Cetus;

        public bool IsDay { get; set; }

        public string State { get; set; }

        public string TimeLeft { get; set; }


        public CetusCycleViewModel(CetusCycle cetusCycle)
        {
            Id = cetusCycle.Id;
            Activation = cetusCycle.Activation;
            Expiry = cetusCycle.Expiry;

            IsDay = cetusCycle.IsDay;
            State = cetusCycle.State;
            TimeLeft = cetusCycle.TimeLeft;

            InitialiseTimer();
        }
    }
}
