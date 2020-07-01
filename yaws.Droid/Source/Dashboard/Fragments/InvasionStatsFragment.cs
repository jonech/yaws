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
    public class InvasionStatsFragment : StatsFragment
    {
        public override string Title => "Invasion";

        protected override void OnWorldStateDataChanged(WorldStateViewModel worldState)
        {
            StatsRecyclerAdapter.SetItems(worldState.Invasions.Cast<ViewModelBase>().ToList());
        }
    }
}