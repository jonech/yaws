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
using System.Reactive.Concurrency;

namespace yaws.Android.Source.Dashboard
{

    public abstract class StatViewHolder : RecyclerView.ViewHolder
    {
        protected StatViewHolder(View itemView) : base(itemView)
        {
        }

        public abstract void Bind(ViewModelBase item);
    }


    public class CetusCycleViewHolder : StatViewHolder
    {
        private readonly TextView title;
        private readonly TextView timeLeft;
        private readonly TextView status;

        private CetusCycleViewModel viewModel;
        IDisposable Disposable;

        public CetusCycleViewHolder(View itemView) : base(itemView)
        {
            title = itemView.FindViewById<TextView>(Resource.Id.text_cetus);
            timeLeft = itemView.FindViewById<TextView>(Resource.Id.text_cetus_time_left);
            status = itemView.FindViewById<TextView>(Resource.Id.text_cetus_status);
        }

        public override void Bind(ViewModelBase item)
        {
            if (item is CetusCycleViewModel model)
            {
                ClearDisposable();

                viewModel = model;

                title.Text = "Cetus Cycle";
                status.Text = viewModel.IsDay ? "Day" : "Night";

                Disposable = viewModel.TimeLeftObservable.Subscribe(timeLeft =>
                {
                    this.timeLeft.Text = $"{timeLeft.Hours}h {timeLeft.Minutes}m {timeLeft.Seconds}s";
                });
            }
        }

        private void ClearDisposable()
        {
            if (viewModel != null)
                viewModel.Dispose();

            if (Disposable != null)
            {
                Disposable.Dispose();
                Disposable = null;
            }
        }
    }
}