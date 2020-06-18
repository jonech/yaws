using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModel
{
    public class WorldStateViewModel
    {
        public CetusCycleViewModel CetusCycle { get; set; }

        public WorldStateViewModel(WorldState worldState)
        {
            if (worldState.CetusCycle != null)
                CetusCycle = new CetusCycleViewModel(worldState.CetusCycle);
        }
    }
}
