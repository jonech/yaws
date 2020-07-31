using System;
using System.Collections.Generic;
using System.Linq;

using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.Fragments
{
    public class CommonStatsFragment : StatsFragment
    {
        public override string Title => "Common";

        protected override void OnWorldStateDataChanged(WorldState worldState)
        {
            StatsRecyclerAdapter.SetItems(new List<IStat>
            {
                worldState.EarthCycle,
                worldState.Arbitration,
                worldState.SentientOutpost
            });
        }
    }
}