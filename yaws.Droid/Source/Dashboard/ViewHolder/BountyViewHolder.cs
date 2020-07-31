using System.Linq;

using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class BountyViewHolder : ExpirableStatViewHolder
    {
        protected RecyclerView BountyJobsRecycler;
        protected StatsRecyclerAdapter BountyJobsAdapter;


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

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is SyndicateMission model)
            {
                TitleTextView.Text = "Bounty";
                BountyJobsAdapter.SetItems(model.Jobs.Cast<IStat>().ToList());
            }
        }
    }
}