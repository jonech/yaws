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
}