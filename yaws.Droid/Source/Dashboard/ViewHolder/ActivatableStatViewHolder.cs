using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

using Android.Util;
using Android.Views;
using Android.Widget;
using WarframeStatService.Entity.Base;
using WarframeStatService.Entity.Interface;
using yaws.Droid.Source.Util;
using yaws.Common.Extension;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class ActivatableStatViewHolder : StatViewHolder
    {
        public TextView TimeActivatedTextView { get; protected set; }
        protected IDisposable Disposable;

        public ActivatableStatViewHolder(View itemView) : base(itemView)
        {
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            if (item is ActivatableStat model)
            {
                //if (TitleTextView != null)
                //{
                //    TitleTextView.Text = model.Name;
                //}

                if (TimeActivatedTextView != null)
                {
                    ClearDisposable();

                    TimeActivatedTextView.Text = model.TimeElapsed.ToFormattedString();

                    Disposable = Observable.Interval(TimeSpan.FromSeconds(1))
                        .Select(p => model.TimeElapsed)
                        .TakeUntil(time => TimeActivatedTextView == null)
                        .RunOnUI()
                        .Subscribe(time =>
                        {
#if DEBUG
                            Log.Info($"{GetType().Name}", $"{model.StatType} -> {time.ToFormattedString()}");
#endif
                            TimeActivatedTextView.Text = time.ToFormattedString();
                        },
                        () =>
                        {
                            if (TimeActivatedTextView != null)
                                TimeActivatedTextView.Text = "EXPIRED";
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