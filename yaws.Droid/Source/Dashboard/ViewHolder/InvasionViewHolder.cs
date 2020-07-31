using Android.Support.Design.Chip;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using WarframeStatService.Entity.Interface;
using WarframeStatService.Entity;
using WarframeStatService.Constant;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class InvasionViewHolder : ActivatableStatViewHolder
    {
        protected TextView NodeTextView;
        protected TextView DescriptionTextView;
        protected TextView AttackerTextView;
        protected TextView AttackerProgressTextView;
        protected TextView DefenderTextView;
        protected TextView DefenderProgressTextView;
        protected Chip AttackerRewardChip;
        protected Chip DefenderRewardChip;

        public InvasionViewHolder(View itemView) : base(itemView)
        {
            TimeActivatedTextView = itemView.FindViewById<TextView>(Resource.Id.text_invasion_time);
            NodeTextView = itemView.FindViewById<TextView>(Resource.Id.text_invasion_node);
            DescriptionTextView = itemView.FindViewById<TextView>(Resource.Id.text_invasion_description);
            AttackerTextView = itemView.FindViewById<TextView>(Resource.Id.text_invasion_attacker);
            AttackerProgressTextView = itemView.FindViewById<TextView>(Resource.Id.text_invasion_attacker_progress);
            AttackerRewardChip = itemView.FindViewById<Chip>(Resource.Id.chip_invasion_attacker_reward);
            DefenderTextView = itemView.FindViewById<TextView>(Resource.Id.text_invasion_defender);
            DefenderProgressTextView = itemView.FindViewById<TextView>(Resource.Id.text_invasion_defender_progress);
            DefenderRewardChip = itemView.FindViewById<Chip>(Resource.Id.chip_invasion_defender_reward);
        }

        public override void Bind(IStat item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is Invasion viewModel)
            {
                NodeTextView.Text = viewModel.Node;
                DescriptionTextView.Text = viewModel.Description;
                AttackerTextView.Text = viewModel.AttackingFaction;
                DefenderTextView.Text = viewModel.DefendingFaction;

                if (viewModel.VSInfestation)
                    AttackerRewardChip.Visibility = ViewStates.Invisible;
                else
                    AttackerRewardChip.Visibility = ViewStates.Visible;

                AttackerRewardChip.Text = viewModel.AttackerReward.ItemString;
                DefenderRewardChip.Text = viewModel.DefenderReward.ItemString;

                AttackerProgressTextView.SetBackgroundColor(GetFactionColor(viewModel.AttackingFaction));
                DefenderProgressTextView.SetBackgroundColor(GetFactionColor(viewModel.DefendingFaction));
                AttackerProgressTextView.LayoutParameters = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.MatchParent, viewModel.Completion/100);
                DefenderProgressTextView.LayoutParameters = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.MatchParent, 1 - (viewModel.Completion/100));
            }
        }

        private Color GetFactionColor(string faction)
        {
            switch (faction)
            {
                case Faction.Grineer:
                    return Color.IndianRed;
                case Faction.Corpus:
                    return Color.CornflowerBlue;
                case Faction.Infested:
                    return Color.SeaGreen;
                default:
                    break;
            }
            return Color.WhiteSmoke;
        }
    }
}