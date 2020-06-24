using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.ViewModel;
using System.Reactive.Linq;
using yaws.Android.Source.Util;

namespace yaws.Android.Source.Dashboard.ViewHolder
{

    public abstract class StatViewHolder : RecyclerView.ViewHolder
    {
        public TextView TitleTextView { get; protected set; }

        protected StatViewHolder(View itemView) : base(itemView)
        {
        }

        public abstract void Bind(ViewModelBase item);
    }

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

                    this.TimeLeftTextView.Text = model.CurrentTimeLeft.ToString("h'h 'm'm 's's'");

                    Disposable = model.TimeLeftObservable
                        .TakeUntil(time => time < TimeSpan.Zero)
                        .RunOnUI()
                        .Subscribe(timeLeft =>
                        {
                            this.TimeLeftTextView.Text = timeLeft.ToString("h'h 'm'm 's's'");
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