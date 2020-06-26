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
using yaws.Core.ViewModel;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class SentientOutpostViewHolder : ExpirableStatViewHolder
    {
        protected TextView NodeTextView;
        protected TextView FactionTextView;
        protected TextView TypeTextView;

        private SentientOutpostViewModel viewModel;

        public SentientOutpostViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_time);
            NodeTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_node);
            FactionTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_faction);
            TypeTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_type);
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is SentientOutpostViewModel model)
            {
                viewModel = model;

                NodeTextView.Text = viewModel.Node;
                FactionTextView.Text = viewModel.Faction;
                TypeTextView.Text = viewModel.Type;
            }
        }
    }
}