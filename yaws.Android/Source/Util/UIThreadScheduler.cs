﻿using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;

using Android.OS;

namespace yaws.Android.Source.Util
{
    public class UIThreadScheduler : IScheduler
    {
        public static IScheduler MainThreadScheduler = new UIThreadScheduler(new Handler(Looper.MainLooper), Looper.MainLooper.Thread.Id);

        Handler handler;
        long looperId;

        public UIThreadScheduler(Handler handler, long? threadIdAssociatedWithHandler)
        {
            this.handler = handler;
            looperId = threadIdAssociatedWithHandler ?? -1;
        }

        public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action)
        {
            bool isCancelled = false;
            var innerDisp = new SerialDisposable() { Disposable = Disposable.Empty };

            if (looperId > 0 && looperId == Java.Lang.Thread.CurrentThread().Id)
            {
                return action(this, state);
            }

            handler.Post(() => {
                if (isCancelled) return;
                innerDisp.Disposable = action(this, state);
            });

            return new CompositeDisposable(
                Disposable.Create(() => isCancelled = true),
                innerDisp);
        }

        public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            bool isCancelled = false;
            var innerDisp = new SerialDisposable() { Disposable = Disposable.Empty };

            handler.PostDelayed(() => {
                if (isCancelled) return;
                innerDisp.Disposable = action(this, state);
            }, dueTime.Ticks / 10 / 1000);

            return new CompositeDisposable(
                Disposable.Create(() => isCancelled = true),
                innerDisp);
        }

        public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            if (dueTime <= Now)
            {
                return Schedule(state, action);
            }

            return Schedule(state, dueTime - Now, action);
        }

        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }
    }
}