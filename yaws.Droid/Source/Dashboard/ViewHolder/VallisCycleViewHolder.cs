using Android.Views;
using Android.Widget;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;
using yaws.Common.Extension;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class VallisCycleViewHolder : ExpirableStatViewHolder
    {
        protected TextView StatusTextView;

        public VallisCycleViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_vallis_cycle_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_vallis_cycle_time);
            StatusTextView = itemView.FindViewById<TextView>(Resource.Id.text_vallis_cycle_status);

            TitleTextView.Text = "Orb Vallis Cycle";
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is VallisCycle model)
            {
                StatusTextView.Text = model.State.ToUpperFirst();
            }

        }
    }
}