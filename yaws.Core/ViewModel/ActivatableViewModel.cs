using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace yaws.Core.ViewModel
{
    /// <summary>
    /// ViewModel with Activation time.
    /// </summary>
    public abstract class ActivatableViewModel : ViewModelBase
    {
        /// <summary>
        /// Activation. WarframeStat's field.
        /// </summary>
        public DateTime Activation { get; set; }

        /// <summary>
        /// Observable that ticks every second.
        /// Get the most updated CurrentTimeSinceActivated.
        /// </summary>
        public IObservable<TimeSpan> TimeSinceActivateObservable { get; private set; }

        /// <summary>
        /// Time since the stat was activated.
        /// </summary>
        public TimeSpan CurrentTimeSinceActivated => DateTime.UtcNow - Activation;

        public ActivatableViewModel(IActivatable model) : base()
        {
            Activation = model.Activation;

            TimeSinceActivateObservable = Observable.Interval(TimeSpan.FromSeconds(1))
                                                    .Select(p => CurrentTimeSinceActivated);
        }
    }
}
