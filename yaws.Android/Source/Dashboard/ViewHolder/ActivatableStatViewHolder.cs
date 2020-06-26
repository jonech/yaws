using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Core.ViewModel;
using yaws.Android.Source.Util;

namespace yaws.Android.Source.Dashboard.ViewHolder
{
    public class ActivatableStatViewHolder : StatViewHolder
    {
        public TextView TimeActivatedTextView { get; protected set; }
        protected IDisposable Disposable;

        public ActivatableStatViewHolder(View itemView) : base(itemView)
        {
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            if (item is ActivatableViewModel model)
            {
                if (TitleTextView != null)
                {
                    TitleTextView.Text = model.Name;
                }

                if (TimeActivatedTextView != null)
                {
                    ClearDisposable();

                    TimeActivatedTextView.Text = model.CurrentTimeSinceActivated.ToFormattedString();

                    Disposable = model.TimeSinceActivateObservable
                        .TakeUntil(time => TimeActivatedTextView == null)
                        .RunOnUI()
                        .Subscribe(time =>
                        {
#if DEBUG
                            Log.Info($"{GetType().Name}", $"{model.Name} -> {time.ToFormattedString()}");
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