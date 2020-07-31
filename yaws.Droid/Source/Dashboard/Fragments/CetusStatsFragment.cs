using System;
using System.Collections.Generic;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.Fragments
{
    public class CetusStatsFragment : StatsFragment
    {
        public override string Title => "Cetus";

        protected override void OnWorldStateDataChanged(WorldState worldState)
        {
            StatsRecyclerAdapter.SetItems(new List<IStat>
            {
                worldState.CetusCycle,
                worldState.CetusBounty
            });
        }
    }
}