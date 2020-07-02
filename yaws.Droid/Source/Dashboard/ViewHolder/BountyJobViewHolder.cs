using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Chip;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using yaws.Core.ViewModel;

namespace yaws.Droid.Source.Dashboard.ViewHolder
{
    public class BountyJobViewHolder : StatViewHolder
    {
        protected TextView EnemyLevelsTextView;
        protected TextView StandingsTextView;
        protected ChipGroup RewardsChipGroup;

        public BountyJobViewHolder(View itemView) : base(itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.text_bounty_job_title);
            EnemyLevelsTextView = itemView.FindViewById<TextView>(Resource.Id.text_bounty_job_enemy_lvls);
            StandingsTextView = itemView.FindViewById<TextView>(Resource.Id.text_bounty_job_standings);
            RewardsChipGroup = itemView.FindViewById<ChipGroup>(Resource.Id.chip_group_bounty_job_rewards);
        }

        public override void Bind(ViewModelBase item, StatsRecyclerAdapter adapter)
        {
            if (item is BountyJobViewModel model)
            {
                TitleTextView.Text = model.Type;
                EnemyLevelsTextView.Text = $"Level {model.MinEnemyLevel} - {model.MaxEnemyLevel}";
                StandingsTextView.Text = model.TotalStanding.ToString();

                RewardsChipGroup.RemoveAllViews();
                foreach (var reward in model.RewardPool)
                {
                    RewardsChipGroup.AddView(GetChip(reward));
                }
            }
        }

        private Chip GetChip(string item)
        {
            var chip = new Chip(ItemView.Context, null, Resource.Attribute.ChipStyle);
            chip.Text = item;

            return chip;
        }
    }
}