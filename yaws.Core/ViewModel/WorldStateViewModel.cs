using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Entity;
using yaws.Core.Constant;

namespace yaws.Core.ViewModel
{
    public class WorldStateViewModel
    {
        public CetusCycleViewModel CetusCycle { get; set; }

        public ArbitrationViewModel Arbitration { get; set; }
        public EarthCycleViewModel EarthCycle { get; set; }
        public SentientOutpostViewModel SentientOutpost { get; set; }
        public VallisCycleViewModel VallisCycle { get; set; }

        public BountyViewModel CetusBounty { get; set; }
        public BountyViewModel VallisBounty { get; set; }
        //public List<FactionMissionViewModel> FactionMissions { get; set; }

        public List<FissureViewModel> Fissures { get; set; }
        public List<InvasionViewModel> Invasions { get; set; }
        public WorldStateViewModel(WorldState worldState)
        {
            if (worldState.CetusCycle != null)
                CetusCycle = new CetusCycleViewModel(worldState.CetusCycle);

            if (worldState.Arbitration != null)
                Arbitration = new ArbitrationViewModel(worldState.Arbitration);

            if (worldState.EarthCycle != null)
                EarthCycle = new EarthCycleViewModel(worldState.EarthCycle);

            if (worldState.SentientOutpost != null)
                SentientOutpost = new SentientOutpostViewModel(worldState.SentientOutpost);

            if (worldState.VallisCycle != null)
                VallisCycle = new VallisCycleViewModel(worldState.VallisCycle);

            //FactionMissions = new List<FactionMissionViewModel>();
            if (worldState.SyndicateMissions != null)
            {
                foreach (var mission in worldState.SyndicateMissions)
                {
                    if (mission.Syndicate == Syndicate.Ostrons)
                        CetusBounty = new BountyViewModel(mission);

                    else if (mission.Syndicate == Syndicate.SolarisUnited)
                        VallisBounty = new BountyViewModel(mission);
                    //else if (Syndicate.Factions.Contains(mission.Syndicate))
                    //    FactionMissions.Add(new FactionMissionViewModel(mission));
                }
            }

            if (worldState.Fissures != null)
            {
                Fissures = worldState.Fissures.Select(x => new FissureViewModel(x)).OrderBy(x => x.TierNum).ToList();
            }

            if (worldState.Invasions != null)
            {
                Invasions = worldState.Invasions.Where(x => !x.Completed).Select(x => new InvasionViewModel(x)).ToList();
            }
        }
    }
}
