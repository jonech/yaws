using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public abstract class TimerViewModel : ViewModelBase
    {
        protected Timer Timer { get; private set; }

        private IObservable<long> timerObservable;
        private IDisposable disposable;
        public TimerViewModel()
        {
            //InitialiseTimer();

            timerObservable = Observable.Interval(TimeSpan.FromSeconds(1));
        }

        private void InitialiseTimer()
        {
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Elapsed += TimerElapsed;
        }

        protected void StartTimer()
        {
            //if (Timer != null) Timer.Start();
            if (timerObservable != null)
                disposable = timerObservable.Subscribe(_ => TimerElapsed(null, null));
        }

        public abstract void TimerElapsed(object sender, ElapsedEventArgs e);


        public virtual void Dispose()
        {
            //if (Timer != null)
            //    Timer.Dispose();

            //Timer = null;

            if (disposable != null) disposable.Dispose();
            disposable = null;
        }
    }
}
