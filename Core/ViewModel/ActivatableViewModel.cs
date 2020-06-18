using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public abstract class ActivatableViewModel : TimerViewModel
    {
        public DateTime Activation { get; set; }

        private BehaviorSubject<TimeSpan> TimeSinceActivateSubject;
        public IObservable<TimeSpan> TimeSinceActivateObservable { get; private set; }

        public ActivatableViewModel(IActivatable model) : base()
        {
            Activation = model.Activation;

            TimeSinceActivateSubject = new BehaviorSubject<TimeSpan>(DateTime.UtcNow - Activation);
            TimeSinceActivateObservable = TimeSinceActivateSubject.AsObservable();
        }

        public override void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            TimeSinceActivateSubject.OnNext(DateTime.UtcNow - Activation);
        }
    }
}
