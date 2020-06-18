using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public abstract class TimerViewModel : ViewModelBase
    {
        protected Timer Timer { get; private set; }

        public TimerViewModel()
        {
            InitialiseTimer();
        }

        private void InitialiseTimer()
        {
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Elapsed += TimerElapsed;
        }

        protected void StartTimer()
        {
            if (Timer != null) Timer.Start();
        }

        public abstract void TimerElapsed(object sender, ElapsedEventArgs e);


        public virtual void Dispose()
        {
            if (Timer != null)
                Timer.Dispose();

            Timer = null;
        }
    }
}
