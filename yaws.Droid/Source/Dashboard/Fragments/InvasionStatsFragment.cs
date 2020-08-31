using System;
using System.Collections.Generic;
using System.Linq;

using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.Fragments
{
    public class InvasionStatsFragment : StatsFragment
    {
        public override string Title => "Invasion";

        protected override void OnWorldStateDataChanged(WorldState worldState)
        {
            StatsRecyclerAdapter.SetItems(worldState.Invasions.Where(i => !i.Completed).Cast<IStat>().ToList());
        }
    }
}