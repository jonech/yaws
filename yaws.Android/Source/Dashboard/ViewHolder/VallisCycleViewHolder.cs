﻿using System;
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
    public class VallisCycleViewHolder : ExpirableStatViewHolder
    {
        protected TextView StatusTextView;

        private VallisCycleViewModel viewModel;

        public VallisCycleViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_vallis_cycle_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_vallis_cycle_time);
            StatusTextView = itemView.FindViewById<TextView>(Resource.Id.text_vallis_cycle_status);
        }

        public override void Bind(ViewModelBase item)
        {
            base.Bind(item);

            if (item is VallisCycleViewModel model)
            {
                viewModel = model;
                StatusTextView.Text = viewModel.State;
            }

        }
    }
}