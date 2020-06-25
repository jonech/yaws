using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModel
{
    public class BountyJobViewModel : ViewModelBase
    {
        public override StatType StatType => StatType.BountyJob;
        public override string Name => "Bounty Job";
        public string Type { get; set; }
        public int TotalStanding { get; set; }
        public int MinEnemyLevel { get; set; }
        public int MaxEnemyLevel { get; set; }
        public int MinMR { get; set; }
        public List<string> RewardPool { get; set; }

        public BountyJobViewModel()
        {

        }
    }
}
