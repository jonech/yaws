using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Autofac;
using ReactiveUI;
using Core;
using Core.ViewModel;
using yaws.Android.Source.Util;

using System.Reactive;
using Android.Support.V4.Widget;
using System.Reactive.Threading.Tasks;

namespace yaws.Android.Source.Dashboard
{
    public class DashboardFragment : Fragment, SwipeRefreshLayout.IOnRefreshListener
    {
        RecyclerView statsRecyclerView;
        StatsRecyclerAdapter statsRecyclerAdapter;
        SwipeRefreshLayout refreshLayout;
        WorldStateRepository wsRepo;
        IDisposable Disposable;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            using (var scope = App.Container.BeginLifetimeScope())
            {
                wsRepo = scope.Resolve<WorldStateRepository>();
            }

            statsRecyclerAdapter = new StatsRecyclerAdapter();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.fragment_dashboard, container, false);

            statsRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_stats);
            statsRecyclerView.SetAdapter(statsRecyclerAdapter);
            statsRecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context));

            refreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_refresh_dashboard);
            refreshLayout.SetOnRefreshListener(this);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            FetchData();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Disposable.Dispose();
        }

        public void OnRefresh()
        {
            FetchData();
        }

        private void FetchData()
        {
            refreshLayout.Refreshing = true;
            Disposable = wsRepo.GetWorldState()
                .ToObservable()
                .DoOnBackgroundThenHandleOnUI()
                .Subscribe((worldState) =>
                {
                    statsRecyclerAdapter.SetItems(new List<ViewModelBase>
                    {
                        worldState.EarthCycle,
                        worldState.CetusCycle,
                        worldState.VallisCycle,
                        worldState.Arbitration,
                        worldState.SentientOutpost
                    });
                },
                err =>
                {
                    Toast.MakeText(this.Context, err.ToString(), ToastLength.Short).Show();
                },
                () =>
                {
                    refreshLayout.Refreshing = false;
                });
        }
    }
}