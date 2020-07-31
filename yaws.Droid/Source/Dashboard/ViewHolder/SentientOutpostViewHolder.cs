using Android.Views;
using Android.Widget;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;


namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class SentientOutpostViewHolder : ExpirableStatViewHolder
    {
        protected TextView NodeTextView;
        protected TextView FactionTextView;
        protected TextView TypeTextView;

        public SentientOutpostViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_time);
            NodeTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_node);
            FactionTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_faction);
            TypeTextView = itemView.FindViewById<TextView>(Resource.Id.text_sentient_type);

            TitleTextView.Text = "Sentient Anomaly";
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is SentientOutpost model)
            {
                if (model.Mission != null)
                {
                    NodeTextView.Text = model.Mission.Node;
                    FactionTextView.Text = model.Mission.Faction;
                    TypeTextView.Text = model.Mission.Type;
                }
            }
        }
    }
}