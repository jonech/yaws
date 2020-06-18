using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public abstract class ExpirableViewModel : TimerViewModel
    {
        public DateTime Expiry { get; set; }

        public DateTime Activation { get; set; }

        private BehaviorSubject<TimeSpan> timeLeftSubject;
        public IObservable<TimeSpan> TimeLeftObservable { get; private set; }

        public ExpirableViewModel(IExpirable model) : base()
        {
            Activation = model.Activation;
            Expiry = model.Expiry;

            timeLeftSubject = new BehaviorSubject<TimeSpan>(Expiry - DateTime.UtcNow);
            TimeLeftObservable = timeLeftSubject.AsObservable();
        }

        public override void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            timeLeftSubject.OnNext(Expiry - DateTime.UtcNow);
        }
    }
}
