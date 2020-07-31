using Android.Views;
using Android.Widget;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class ArbitrationViewHolder : ExpirableStatViewHolder
    {
        protected TextView NodeTextView;
        protected TextView EnemyTextView;
        protected TextView TypeTextView;

        public ArbitrationViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_title);
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_time_left);
            NodeTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_node);
            EnemyTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_enemy);
            TypeTextView = itemView.FindViewById<TextView>(Resource.Id.text_arbi_type);

            TitleTextView.Text = "Arbitration";
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is Arbitration viewModel)
            {
                NodeTextView.Text = viewModel.Node;
                EnemyTextView.Text = viewModel.Enemy;
                TypeTextView.Text = viewModel.Type;
            }
        }

    }
}