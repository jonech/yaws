using System;
using System.Reactive.Linq;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Content.Res;
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

                    UpdateTimeLeftText(TimeLeftTextView, model.CurrentTimeLeft);

                    Disposable = model.TimeLeftObservable
                        .TakeUntil(time => time < TimeSpan.Zero || TimeLeftTextView == null)
                        .RunOnUI()
                        .Subscribe(timeLeft =>
                        {
#if DEBUG
                            Log.Info($"{GetType().Name}", $"{model.Name} -> {timeLeft.ToFormattedString()}");
#endif
                            UpdateTimeLeftText(TimeLeftTextView, model.CurrentTimeLeft);
                        },
                        () =>
                        {
                            ClearDisposable();
                        });

                    adapter.AddTimeSubscription(Disposable);
                }
            }
        }

        private void UpdateTimeLeftText(TextView textView, TimeSpan timeLeft)
        {
            textView.Text = timeLeft.ToFormattedString();

            var colorStateList = ContextCompat.GetColorStateList(ItemView.Context, GetTimeLeftTextColor(timeLeft));
            textView.SetTextColor(colorStateList);
        }


        protected virtual int GetTimeLeftTextColor(TimeSpan timeLeft)
        {
            if (timeLeft == null || timeLeft < TimeSpan.Zero)
                return Resource.Color.time_less_than_5min;
            else if (timeLeft >= TimeSpan.FromMinutes(10))
                return Resource.Color.time_more_than_10min;
            else if (timeLeft >= TimeSpan.FromMinutes(5))
                return Resource.Color.time_less_than_10min;
            else
                return Resource.Color.time_less_than_5min;
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