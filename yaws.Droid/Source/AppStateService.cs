using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using yaws.Core;
using yaws.Core.ViewModel;
using yaws.Droid.Source.Util;

namespace yaws.Droid.Source
{
    public class AppStateService
    {
        private WorldStateRepository repository;

        private BehaviorSubject<WorldStateViewModel> worldStateSubject;
        public IObservable<WorldStateViewModel> WorldStateObservable => worldStateSubject.AsObservable();

        private BehaviorSubject<bool> refreshingSubject;
        public IObservable<bool> RefreshingObservable => refreshingSubject.AsObservable();


        private BehaviorSubject<string> dashboardState;
        public IObservable<string> DashboardStateObservable => dashboardState.AsObservable();
        public string CurrentDashboardFragment
        {
            get => dashboardState.Value;
            set => dashboardState.OnNext(value);
        }

        public AppStateService(WorldStateRepository repository)
        {
            this.repository = repository;
            worldStateSubject = new BehaviorSubject<WorldStateViewModel>(null);
            refreshingSubject = new BehaviorSubject<bool>(false);

            dashboardState = new BehaviorSubject<string>(string.Empty);
        }


        public void FetchWorldState()
        {
            refreshingSubject.OnNext(true);

            var dis = repository.GetWorldState()
                .ToObservable()
                //.DoOnBackgroundThenHandleOnUI()
                .Subscribe((worldState) =>
                {
                    if (worldState != null)
                        worldStateSubject.OnNext(worldState);
                },
                err =>
                {
                    worldStateSubject.OnError(err);
                },
                () =>
                {
                    refreshingSubject.OnNext(false);
                });
        }
    }
}