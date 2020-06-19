using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Core.ViewModel;
using yaws.Android.Source.Util;

namespace yaws.Android.Source.Dashboard.ViewHolder
{
    public class ArbitrationViewHolder : StatViewHolder
    {
        private readonly TextView title;
        private readonly TextView timeLeft;
        private readonly TextView node;
        private readonly TextView enemy;
        private readonly TextView type;


        private ArbitrationViewModel viewModel;
        private IDisposable Disposable;

        public ArbitrationViewHolder(View itemView) : base(itemView)
        {
            title = itemView.FindViewById<TextView>(Resource.Id.text_arbi_title);
            timeLeft = itemView.FindViewById<TextView>(Resource.Id.text_arbi_time_left);
            node = itemView.FindViewById<TextView>(Resource.Id.text_arbi_node);
            enemy = itemView.FindViewById<TextView>(Resource.Id.text_arbi_enemy);
            type = itemView.FindViewById<TextView>(Resource.Id.text_arbi_type);
        }

        public override void Bind(ViewModelBase item)
        {
            if (item is ArbitrationViewModel model)
            {
                ClearDisposable();

                viewModel = model;

                title.Text = "Arbitration";
                node.Text = viewModel.Node;
                enemy.Text = viewModel.Enemy;
                type.Text = viewModel.Type;

                Disposable = viewModel.TimeLeftObservable
                    .DoOnBackgroundThenHandleOnUI()
                    .Subscribe(timeLeft =>
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