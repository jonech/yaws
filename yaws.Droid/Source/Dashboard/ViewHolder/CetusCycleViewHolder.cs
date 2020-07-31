using Android.Views;
using Android.Widget;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class CetusCycleViewHolder : ExpirableStatViewHolder
    {
        protected readonly TextView StatusTextView;

        public CetusCycleViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_cetus_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_cetus_time_left);
            StatusTextView = itemView.FindViewById<TextView>(Resource.Id.text_cetus_status);

            TitleTextView.Text = "Cetus Cycle";
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is CetusCycle model)
            {
                StatusTextView.Text = model.State;
            }
        }

    }
}