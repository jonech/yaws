using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Database;
using Android.OS;
using Java.Lang;
using ReactiveUI;

namespace yaws.Droid.Source.Util
{
    public static class ObservableExtension
    {
        /// <summary>
        /// Put Observable task on to background thread, then once it's completed, handle result on Android UI thread.
        /// </summary>
        /// <typeparam name="T">Type of the Observable result</typeparam>
        /// <param name="obs">Current Observable</param>
        /// <returns></returns>
        public static IObservable<T> DoOnBackgroundThenHandleOnUI<T>(this IObservable<T> obs)
        {
            return obs
                .SubscribeOn(new TaskPoolScheduler(new TaskFactory()))
                .ObserveOn(new HandlerScheduler(new Handler(), Thread.CurrentThread().Id));
        }

        /// <summary>
        /// ObserveOn RxApp.MainThreadScheduler (from ReactiveUI).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obs"></param>
        /// <returns></returns>
        public static IObservable<T> RunOnUI<T>(this IObservable<T> obs)
        {
            return obs
                .ObserveOn(RxApp.MainThreadScheduler);
        }
    }


    /// <summary>
    /// HandlerScheduler is a scheduler that schedules items on a running 
    /// Activity's main thread. This is the moral equivalent of 
    /// DispatcherScheduler.
    /// </summary>
    /// <remarks>
    /// Source code obtained from https://github.com/jetruby/xamarin-android-ios-example/blob/master/Droid/UiThreadScheduler.cs.
    /// Or possibly from https://github.com/reactiveui/ReactiveUI/blob/main/src/ReactiveUI/Platforms/android/HandlerScheduler.cs.
    /// </remarks>
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