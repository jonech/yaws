using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Chip;
using Android.Views;
using Android.Widget;
using yaws.Core.ViewModel;

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

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            base.Bind(item, adapter);

            if (item is InvasionViewModel viewModel)
            {
                NodeTextView.Text = viewModel.Node;
                DescriptionTextView.Text = viewModel.Description;
                AttackerTextView.Text = viewModel.AttackingFaction;
                DefenderTextView.Text = viewModel.DefendingFaction;

                if (viewModel.VSInfestation)
                {
                    AttackerProgressTextView.SetBackgroundColor(Android.Graphics.Color.Red);
                    AttackerRewardChip.Visibility = ViewStates.Invisible;
                }
                else if (viewModel.AttackingFaction == "Grineer")
                {
                    AttackerProgressTextView.SetBackgroundColor(Android.Graphics.Color.Green);
                }
                else
                {
                    AttackerProgressTextView.SetBackgroundColor(Android.Graphics.Color.Blue);
                }

                if (viewModel.DefendingFaction == "Grineer")
                {
                    DefenderProgressTextView.SetBackgroundColor(Android.Graphics.Color.Green);
                }
                else
                {
                    DefenderProgressTextView.SetBackgroundColor(Android.Graphics.Color.Blue);
                }

                AttackerRewardChip.Text = viewModel.AttackerReward.ItemString;
                DefenderRewardChip.Text = viewModel.DefenderReward.ItemString;

                AttackerProgressTextView.LayoutParameters = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.MatchParent, viewModel.Completion/100);
                DefenderProgressTextView.LayoutParameters = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.MatchParent, 1 - (viewModel.Completion/100));
            }
        }
    }
}