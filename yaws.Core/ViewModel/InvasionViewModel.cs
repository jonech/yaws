using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Core.ViewModel
{
    public class InvasionViewModel : ActivatableViewModel
    {
        public override StatType StatType => StatType.Invasion;
        public override string Name => "Invasion";

        public string Node { get; set; }
        public string Description { get; set; }
        public string AttackingFaction { get; set; }
        public InvasionRewardViewModel AttackerReward { get; set; }
        public string DefendingFaction { get; set; }
        public InvasionRewardViewModel DefenderReward { get; set; }
        public bool VSInfestation { get; set; }
        public float Completion { get; set; }

        public InvasionViewModel(Invasion model) : base(model)
        {
            Node = model.Node;
            Description = model.Description;
            AttackingFaction = model.AttackingFaction;
            DefendingFaction = model.DefendingFaction;
            VSInfestation = model.VSInfestation;
            Completion = model.Completion;

            if (model.AttackerReward != null)
            {
                AttackerReward = new InvasionRewardViewModel
                {
                    Thumbnail = model.AttackerReward.Thumbnail,
                    ItemString = model.AttackerReward.ItemString
                };
            }

            if (model.DefenderReward != null)
            {
                DefenderReward = new InvasionRewardViewModel
                {
                    Thumbnail = model.DefenderReward.Thumbnail,
                    ItemString = model.DefenderReward.ItemString
                };
            }
        }
    }

    public class InvasionRewardViewModel
    {
        public string Thumbnail { get; set; }
        public string ItemString { get; set; }
    }
}
