using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public abstract class ExpirableViewModel : ViewModelBase
    {
        public DateTime Expiry { get; set; }

        public DateTime Activation { get; set; }

        public IObservable<TimeSpan> TimeLeftObservable { get; private set; }
        public TimeSpan CurrentTimeLeft => Expiry - DateTime.UtcNow;

        public ExpirableViewModel(IExpirable model) : base()
        {
            Activation = model.Activation;
            Expiry = model.Expiry;

            TimeLeftObservable = Observable.Interval(TimeSpan.FromSeconds(1))
                                           .Select(p => CurrentTimeLeft);
        }

    }
}
