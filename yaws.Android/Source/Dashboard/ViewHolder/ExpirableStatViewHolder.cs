using System;
using System.Reactive.Linq;

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
    public class ExpirableStatViewHolder : StatViewHolder
    {
        public TextView TimeLeftTextView { get; protected set; }
        protected IDisposable Disposable;

        public ExpirableStatViewHolder(View itemView) : base(itemView)
        {
        }

        public override void Bind(ViewModelBase item)
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

                    this.TimeLeftTextView.Text = model.CurrentTimeLeft.ToFormattedString();

                    Disposable = model.TimeLeftObservable
                        .TakeUntil(time => time < TimeSpan.Zero)
                        .RunOnUI()
                        .Subscribe(timeLeft =>
                        {
#if DEBUG
                            Log.Info("ExprTimeLeftObservable", $"{model.Name} -> {timeLeft.ToFormattedString()}");
#endif
                            this.TimeLeftTextView.Text = timeLeft.ToFormattedString();
                        },
                        () =>
                        {
                            this.TimeLeftTextView.Text = "EXPIRED";
                        });
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ClearDisposable();
        }
    }
}