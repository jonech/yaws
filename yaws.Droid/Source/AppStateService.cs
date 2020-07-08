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
        private WorldStateRepository repository;

        private BehaviorSubject<WorldStateViewModel> worldStateSubject;
        public IObservable<WorldStateViewModel> WorldStateObservable => worldStateSubject.AsObservable();

        private BehaviorSubject<bool> refreshingSubject;
        public IObservable<bool> RefreshingObservable => refreshingSubject.AsObservable();

        private BehaviorSubject<string> errorSubject;
        public IObservable<string> ErrorObservable => errorSubject.AsObservable();

        public string Platform { get; set; }

        public AppStateService(WorldStateRepository repository)
        {
            this.repository = repository;
            worldStateSubject = new BehaviorSubject<WorldStateViewModel>(null);
            refreshingSubject = new BehaviorSubject<bool>(false);
            errorSubject = new BehaviorSubject<string>(string.Empty);
            Platform = WFPlatform.PC;
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

            var dis = repository.GetWorldState(Platform)
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