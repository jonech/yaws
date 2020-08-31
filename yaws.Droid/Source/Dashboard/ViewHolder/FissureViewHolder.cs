using Android.Views;
using Android.Widget;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class FissureViewHolder : ExpirableStatViewHolder
    {
        protected TextView TierTextView;
        protected TextView NodeTextView;
        protected TextView EnemyTextView;
        protected TextView MissionTextView;
        protected ImageView TierImageView;

        public FissureViewHolder(View itemView) : base(itemView)
        {
            TimeLeftTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_time);
            TierTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_tier);
            NodeTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_node);
            EnemyTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_enemy);
            MissionTextView = itemView.FindViewById<TextView>(Resource.Id.text_fissure_mission);
            TierImageView = itemView.FindViewById<ImageView>(Resource.Id.image_fissure_tier);
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is Fissure viewModel)
            {
                TierTextView.Text = viewModel.Tier;
                NodeTextView.Text = viewModel.Node;
                EnemyTextView.Text = viewModel.Enemy;
                MissionTextView.Text = viewModel.MissionType;

                TierImageView.SetImageResource(GetTierImage(viewModel.TierNum));
            }
        }

        private int GetTierImage(int tier)
        {
            switch (tier)
            {
                case 1:
                    return Resource.Drawable.ic_lith;
                case 2:
                    return Resource.Drawable.ic_meso;
                case 3:
                    return Resource.Drawable.ic_neo;
                case 4:
                    return Resource.Drawable.ic_axi;
                case 5:
                    return Resource.Drawable.ic_requiem;
                default:
                    return Resource.Drawable.abc_ic_clear_material;
            }
        }
    }
}