using Android.OS;
using System.Reactive.Concurrency;

namespace yaws.Android.Source.Util
{
    public class UiThreadScheduler : IScheduler
    {
        private Handler handler;
        private long id;

        public UiThreadScheduler(Handler handler, long id)
        {
            this.handler = handler;
            this.id = id;
        }
    }
}