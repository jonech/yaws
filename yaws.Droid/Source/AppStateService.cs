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
using yaws.Core.Constant;
using yaws.Core.ViewModel;
using yaws.Droid.Source.Util;

namespace yaws.Droid.Source
{
    public class AppStateService
    {
        private readonly WorldStateRepository repository;
        private readonly AppSettings appSettings;

        private readonly BehaviorSubject<WorldStateViewModel> worldStateSubject;
        public IObservable<WorldStateViewModel> WorldStateObservable => worldStateSubject.AsObservable();
        public WorldStateViewModel CurrentWorldState => worldStateSubject.Value;

        private readonly BehaviorSubject<bool> refreshingSubject;
        public IObservable<bool> RefreshingObservable => refreshingSubject.AsObservable();

        private readonly BehaviorSubject<string> errorSubject;
        public IObservable<string> ErrorObservable => errorSubject.AsObservable();

        public AppStateService(WorldStateRepository repository, AppSettings appSettings)
        {
            this.repository = repository;
            this.appSettings = appSettings;

            worldStateSubject = new BehaviorSubject<WorldStateViewModel>(null);
            refreshingSubject = new BehaviorSubject<bool>(false);
            errorSubject = new BehaviorSubject<string>(string.Empty);
        }


        public void FetchWorldState()
        {
            refreshingSubject.OnNext(true);

            //var dis = repository.GetWorldState()
            //    .ToObservable()
            //    .Subscribe((worldState) =>
            //    {
            //        refreshingSubject.OnNext(false);

            //        if (worldState != null)
            //            worldStateSubject.OnNext(worldState);
            //    },
            //    err =>
            //    {
            //        refreshingSubject.OnNext(false);
            //        worldStateSubject.OnError(err);
            //    });
            var plaform = appSettings.Platform;
            var dis = repository.GetWorldState(plaform)
                .ToObservable()
                .Subscribe((worldState) =>
                {
                    if (worldState != null)
                        worldStateSubject.OnNext(worldState);
                },
                err =>
                {
                    errorSubject.OnNext(err.Message);
                    refreshingSubject.OnNext(false);
                },
                () =>
                {
                    refreshingSubject.OnNext(false);
                });
        }
    }
}