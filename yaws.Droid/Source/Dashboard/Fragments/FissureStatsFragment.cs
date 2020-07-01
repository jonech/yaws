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
    public class FissureStatsFragment : StatsFragment
    {
        public override string Title => "Fissure";

        protected override void OnWorldStateDataChanged(WorldStateViewModel worldState)
        {
            StatsRecyclerAdapter.SetItems(worldState.Fissures.Cast<ViewModelBase>().ToList());
        }
    }
}