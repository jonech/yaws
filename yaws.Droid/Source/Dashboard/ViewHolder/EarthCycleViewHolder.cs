using Android.Views;
using Android.Widget;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class EarthCycleViewHolder : ExpirableStatViewHolder
    {
        protected TextView StatusTextView;

        public EarthCycleViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_earth_cycle_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_earth_cycle_time);
            StatusTextView = itemView.FindViewById<TextView>(Resource.Id.text_earth_cycle_status);

            TitleTextView.Text = "Earth Cycle";
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is EarthCycle viewModel)
            {
                StatusTextView.Text = viewModel.State;
            }

        }
    }
}