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

namespace yaws.Android.Source.Dashboard.ViewHolder
{
    public class ArbitrationViewHolder : ExpirableStatViewHolder
    {
        protected TextView NodeTextView;
        protected TextView EnemyTextView;
        protected TextView TypeTextView;

        private ArbitrationViewModel viewModel;

        public ArbitrationViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_time_left);
            NodeTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_node);
            EnemyTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_enemy);
            TypeTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_type);
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is ArbitrationViewModel model)
            {
                viewModel = model;

                NodeTextView.Text = viewModel.Node;
                EnemyTextView.Text = viewModel.Enemy;
                TypeTextView.Text = viewModel.Type;
            }
        }

    }
}