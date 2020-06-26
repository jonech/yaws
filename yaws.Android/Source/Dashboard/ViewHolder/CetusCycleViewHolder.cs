using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Core.ViewModel;

namespace yaws.Android.Source.Dashboard.ViewHolder
{
    public class CetusCycleViewHolder : ExpirableStatViewHolder
    {
        protected readonly TextView StatusTextView;

        private CetusCycleViewModel viewModel;

        public CetusCycleViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_cetus_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_cetus_time_left);
            StatusTextView = itemView.FindViewById<TextView>(Resource.Id.text_cetus_status);
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is CetusCycleViewModel model)
            {
                viewModel = model;
                StatusTextView.Text = viewModel.IsDay ? "Day" : "Night";
            }
        }

    }
}