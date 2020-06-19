using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModel
{
    public class WorldStateViewModel
    {
        public CetusCycleViewModel CetusCycle { get; set; }

        public ArbitrationViewModel Arbitration { get; set; }
        public EarthCycleViewModel EarthCycle { get; set; }
        public SentientOutpostViewModel SentientOutpost { get; set; }
        public VallisCycleViewModel VallisCycle { get; set; }

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
        }
    }
}
