using System.Linq;
using WarframeStatService.Entity;
using WarframeStatService.Entity.Interface;

namespace yaws.Droid.Source.Dashboard.Fragments
{
    public class FissureStatsFragment : StatsFragment
    {
        public override string Title => "Fissure";

        protected override void OnWorldStateDataChanged(WorldState worldState)
        {
            StatsRecyclerAdapter.SetItems(worldState.Fissures.Cast<IStat>().ToList());
        }
    }
}