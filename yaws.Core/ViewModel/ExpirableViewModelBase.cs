using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace yaws.Core.ViewModel
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

//#if DEBUG
//            var random = new Random();
//            Expiry = DateTime.UtcNow + TimeSpan.FromSeconds(random.Next(5, 21));
//#endif

            TimeLeftObservable = Observable.Interval(TimeSpan.FromSeconds(1))
                                           .Select(p => CurrentTimeLeft);
        }

    }
}
