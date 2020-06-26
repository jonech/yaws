using Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yaws.Core.ViewModel
{
    public class BountyViewModel : ExpirableViewModel
    {
        public override StatType StatType { get; }

        public override string Name { get; }
        public bool IsActive { get; set; }
        public List<BountyJobViewModel> Jobs { get; set; }

        public BountyViewModel(SyndicateMission model) : base(model)
        {
            IsActive = model.Active;

            if (model.Syndicate == Syndicate.Ostrons)
            {
                StatType = StatType.CetusBounty;
                Name = "Cetus Bounty";
            }
            else if (model.Syndicate == Syndicate.SolarisUnited)
            {
                StatType = StatType.VallisBounty;
                Name = "Vallis Bounty";
            }

            if (model.Jobs != null && model.Jobs.Any())
            {
                Jobs = model.Jobs.Select(job =>
                {
                    return new BountyJobViewModel
                    {
                        Type = job.Type,
                        RewardPool = job.RewardPool,
                        TotalStanding = job.StandingStages == null ? 0 : job.StandingStages.Sum(),
                        MaxEnemyLevel = job.EnemyLevels == null ? 0 : job.EnemyLevels.Max(),
                        MinEnemyLevel = job.EnemyLevels == null ? 0 : job.EnemyLevels.Min(),
                        MinMR = job.MinimumMR
                    };
                }).ToList();
            }
        }
    }
}
