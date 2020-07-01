using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using yaws.Core.ViewModel;

namespace yaws.Droid.Source.Dashboard.Fragments
{
    public class CetusStatsFragment : StatsFragment
    {
        public override string Title => "Cetus";

        protected override void OnWorldStateDataChanged(WorldStateViewModel worldState)
        {
            StatsRecyclerAdapter.SetItems(new List<ViewModelBase>
            {
                worldState.CetusCycle,
                worldState.CetusBounty
            });
        }
    }
}