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
    public class FissureViewHolder : ExpirableStatViewHolder
    {
        protected TextView TierTextView;
        protected TextView NodeTextView;
        protected TextView EnemyTextView;
        protected TextView MissionTextView;

        public FissureViewHolder(View itemView) : base(itemView)
        {
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_time);
            TierTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_tier);
            NodeTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_node);
            EnemyTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_enemy);
            MissionTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_mission);
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is FissureViewModel viewModel)
            {
                TierTextView.Text = viewModel.Tier;
                NodeTextView.Text = viewModel.Node;
                EnemyTextView.Text = viewModel.Enemy;
                MissionTextView.Text = viewModel.MissionType;
            }
        }
    }
}