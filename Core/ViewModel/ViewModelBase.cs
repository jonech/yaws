using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public enum StatType
    {
        Arbitration, Cetus, Earth, SentientOutpost, Vallis
    }

    public abstract class ViewModelBase
    {
        public abstract StatType Type { get; }

        public string Id { get; set; }

        //public DateTime Expiry { get; set; }

        //public DateTime Activation { get; set; }

        //public IObservable<TimeSpan> TimeLeftObservable { get; private set; }
        //private BehaviorSubject<TimeSpan> timeLeftSubject;

        //protected Timer Timer { get; private set; }

        //protected void InitialiseTimer()
        //{
        //    Timer = new Timer();
        //    Timer.Interval = 1000;
        //    Timer.Elapsed += TimerElapsed;

        //    timeLeftSubject = new BehaviorSubject<TimeSpan>(Expiry - DateTime.UtcNow);
        //    TimeLeftObservable = timeLeftSubject.AsObservable();

        //    Timer.Start();
        //}

        //private void TimerElapsed(object sender, ElapsedEventArgs e)
        //{
        //    timeLeftSubject.OnNext(Expiry - DateTime.UtcNow);
        //}

        //public void Dispose()
        //{
        //    if (Timer != null)
        //        Timer.Dispose();

        //    Timer = null;
        //}
    }
}
