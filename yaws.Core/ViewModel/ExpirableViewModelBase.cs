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
    /// ViewModel that can be expired.
    /// </summary>
    public abstract class ExpirableViewModel : ViewModelBase
    {
        /// <summary>
        /// Expiry. WarframeStat's field
        /// </summary>
        public DateTime Expiry { get; set; }

        /// <summary>
        /// Activation. WarframeStat's field
        /// </summary>
        public DateTime Activation { get; set; }

        /// <summary>
        /// Observable that ticks every second.
        /// Get the most updated CurrentTimeLeft.
        /// </summary>
        public IObservable<TimeSpan> TimeLeftObservable { get; private set; }

        /// <summary>
        /// How much time left until the stat is expired.
        /// </summary>
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
