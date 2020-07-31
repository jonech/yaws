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

using yaws.Droid.Source.Util;
using WarframeStatService.Entity;

namespace yaws.Droid.Source.Dashboard.Fragments
{
    public abstract class StatsFragment : Fragment, SwipeRefreshLayout.IOnRefreshListener
    {
        public abstract string Title { get; }

        protected RecyclerView StatsRecyclerView { get; set; }
        protected StatsRecyclerAdapter StatsRecyclerAdapter { get; set; }

        SwipeRefreshLayout refreshLayout;
        AppStateService worldStateService;
        IDisposable worldStateSubscription;
        IDisposable refreshSubscription;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            using (var scope = App.Container.BeginLifetimeScope())
            {
                worldStateService = scope.Resolve<AppStateService>();
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
                SubscribeToWorldState();

            if (refreshSubscription == null)
                SubscribeToRefresh();
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


        private void SubscribeToWorldState()
        {
            worldStateSubscription = worldStateService.WorldStateObservable
                .Delay(TimeSpan.FromSeconds(0.2)) // hack: let refresh animation finish to appear less 'jerky'
                .RunOnUI()
                .Subscribe((worldState) =>
                {
                    if (worldState != null)
                        OnWorldStateDataChanged(worldState);
                });
        }

        private void SubscribeToRefresh()
        {
            refreshSubscription = worldStateService.RefreshingObservable
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


        protected abstract void OnWorldStateDataChanged(WorldState worldState);
    }
}