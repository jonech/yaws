using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;

using Autofac;

using yaws.Core;
using yaws.Core.ViewModel;
using yaws.Droid.Source.Util;


namespace yaws.Droid.Source.Dashboard
{
    public abstract class StatsFragment : Fragment, SwipeRefreshLayout.IOnRefreshListener
    {
        public abstract string Title { get; }

        protected RecyclerView StatsRecyclerView { get; set; }
        protected StatsRecyclerAdapter StatsRecyclerAdapter { get; set; }

        SwipeRefreshLayout refreshLayout;
        WorldStateService worldStateService;
        IDisposable worldStateSubscription;
        IDisposable refreshSubscription;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            using (var scope = App.Container.BeginLifetimeScope())
            {
                worldStateService = scope.Resolve<WorldStateService>();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_stats, container, false);

            StatsRecyclerAdapter = new StatsRecyclerAdapter();

            StatsRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_stats);
            StatsRecyclerView.SetAdapter(StatsRecyclerAdapter);
            StatsRecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context));

            refreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_refresh_stats);
            refreshLayout.SetOnRefreshListener(this);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            if (worldStateSubscription == null)
                subscribeToWorldState();

            if (refreshSubscription == null)
                subscribeToRefresh();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            DisposeSubscription();
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            DisposeSubscription();
        }

        public void OnRefresh()
        {
            worldStateService.FetchWorldState();
        }


        private void subscribeToWorldState()
        {
            worldStateSubscription = worldStateService.WorldStateObservable
                .Where(worldState => worldState != null)
                .Retry()
                .RunOnUI()
                .Subscribe((worldState) =>
                {
                    OnWorldStateDataChanged(worldState);
                },
                err =>
                {
                    Toast.MakeText(this.Context, err.ToString(), ToastLength.Short).Show();
                });
        }

        private void subscribeToRefresh()
        {
            refreshSubscription = worldStateService.RefreshingObservable
                .Delay(TimeSpan.FromSeconds(1))
                .RunOnUI()
                .Subscribe(refresh => refreshLayout.Refreshing = refresh);
        }

        private void DisposeSubscription()
        {
            if (worldStateSubscription != null)
            {
                worldStateSubscription.Dispose();
                worldStateSubscription = null;
            }

            if (refreshSubscription != null)
            {
                refreshSubscription.Dispose();
                refreshSubscription = null;
            }

            if (StatsRecyclerAdapter != null)
                StatsRecyclerAdapter.DisposeSubscriptions();
        }


        protected abstract void OnWorldStateDataChanged(WorldStateViewModel worldState);
    }


    public class CommonStatsFragment : StatsFragment
    {
        public override string Title => "Common";

        protected override void OnWorldStateDataChanged(WorldStateViewModel worldState)
        {
            StatsRecyclerAdapter.SetItems(new List<ViewModelBase>
            {
                worldState.EarthCycle,
                worldState.Arbitration,
                worldState.SentientOutpost
            });
        }
    }

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

    public class VallisStatsFragment : StatsFragment
    {
        public override string Title => "Vallis";

        protected override void OnWorldStateDataChanged(WorldStateViewModel worldState)
        {
            StatsRecyclerAdapter.SetItems(new List<ViewModelBase>
            {
                worldState.VallisCycle,
                worldState.VallisBounty
            });
        }
    }

    public class FissureStatsFragment : StatsFragment
    {
        public override string Title => "Fissure";

        protected override void OnWorldStateDataChanged(WorldStateViewModel worldState)
        {
            StatsRecyclerAdapter.SetItems(worldState.Fissures.Cast<ViewModelBase>().ToList());
        }
    }

    public class InvasionStatsFragment : StatsFragment
    {
        public override string Title => "Invasion";

        protected override void OnWorldStateDataChanged(WorldStateViewModel worldState)
        {
            StatsRecyclerAdapter.SetItems(worldState.Fissures.Cast<ViewModelBase>().ToList());
        }
    }
}