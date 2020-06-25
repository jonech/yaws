using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.ViewModel;

namespace yaws.Android.Source.Dashboard.ViewHolder
{
    public class BountyViewHolder : ExpirableStatViewHolder
    {
        protected RecyclerView BountyJobsRecycler;
        protected StatsRecyclerAdapter BountyJobsAdapter;

        private BountyViewModel viewModel;


        public BountyViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_bounty_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_bounty_time);
            BountyJobsRecycler = itemView.FindViewById<RecyclerView>(Resource.Id.recycler_bounty_jobs);

            BountyJobsAdapter = new StatsRecyclerAdapter();
            BountyJobsRecycler.SetAdapter(BountyJobsAdapter);
            var layoutManager = new LinearLayoutManager(itemView.Context);
            BountyJobsRecycler.SetLayoutManager(layoutManager);

            var divider = new DividerItemDecoration(itemView.Context, layoutManager.Orientation);
            BountyJobsRecycler.AddItemDecoration(divider);
        }

        public override void Bind(ViewModelBase item)
        {
            base.Bind(item);

            if (item is BountyViewModel model)
            {
                viewModel = model;

                BountyJobsAdapter.SetItems(model.Jobs.Cast<ViewModelBase>().ToList());
            }
        }
    }
}