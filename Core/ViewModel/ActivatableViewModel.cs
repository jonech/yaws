using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public abstract class ActivatableViewModel : ViewModelBase
    {
        public DateTime Activation { get; set; }
        public IObservable<TimeSpan> TimeSinceActivateObservable { get; private set; }
        public TimeSpan CurrentTimeSinceActivated => DateTime.UtcNow - Activation;

        public ActivatableViewModel(IActivatable model) : base()
        {
            Activation = model.Activation;

            TimeSinceActivateObservable = Observable.Interval(TimeSpan.FromSeconds(1))
                                                    .Select(p => CurrentTimeSinceActivated);
        }
    }
}
