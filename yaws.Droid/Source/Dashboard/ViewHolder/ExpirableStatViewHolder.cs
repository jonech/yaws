using System;
using System.Reactive.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using yaws.Core.ViewModel;

using yaws.Droid.Source.Util;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class ExpirableStatViewHolder : StatViewHolder
    {
        public TextView TimeLeftTextView { get; protected set; }
        protected IDisposable Disposable;

        public ExpirableStatViewHolder(View itemView) : base(itemView)
        {
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            if (item is ExpirableViewModel model)
            {
                if (TitleTextView != null)
                {
                    TitleTextView.Text = model.Name;
                }

                if (TimeLeftTextView != null)
                {
                    ClearDisposable();

                    TimeLeftTextView.Text = model.CurrentTimeLeft.ToFormattedString();

                    Disposable = model.TimeLeftObservable
                        .TakeUntil(time => time < TimeSpan.Zero || TimeLeftTextView == null)
                        .RunOnUI()
                        .Subscribe(timeLeft =>
                        {
#if DEBUG
                            Log.Info($"{GetType().Name}", $"{model.Name} -> {timeLeft.ToFormattedString()}");
#endif
                            TimeLeftTextView.Text = timeLeft.ToFormattedString();
                        },
                        () =>
                        {
                            if (TimeLeftTextView != null)
                                TimeLeftTextView.Text = "EXPIRED";
                        });

                    adapter.AddTimeSubscription(Disposable);
                }
            }
        }

        private void ClearDisposable()
        {
            if (Disposable != null)
            {
                Disposable.Dispose();
                Disposable = null;
            }
        }
    }
}