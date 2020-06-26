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
    public class EarthCycleViewHolder : ExpirableStatViewHolder
    {
        protected TextView StatusTextView;

        private EarthCycleViewModel viewModel;

        public EarthCycleViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_earth_cycle_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_earth_cycle_time);
            StatusTextView = itemView.FindViewById<TextView>(Resource.Id.text_earth_cycle_status);
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is EarthCycleViewModel model)
            {
                viewModel = model;
                StatusTextView.Text = viewModel.State;
            }

        }
    }
}