using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using WarframeStatService.API;
using WarframeStatService.Entity;
using yaws.Core;

namespace yaws.Droid.Source
{
    public class AppStateService
    {
        private readonly AppSettings appSettings;
        private readonly IWarframeStatAPI api;

        private readonly BehaviorSubject<WorldState> worldStateSubject;
        public IObservable<WorldState> WorldStateObservable => worldStateSubject.AsObservable();
        public WorldState CurrentWorldState => worldStateSubject.Value;

        private readonly BehaviorSubject<bool> refreshingSubject;
        public IObservable<bool> RefreshingObservable => refreshingSubject.AsObservable();

        private readonly BehaviorSubject<string> errorSubject;
        public IObservable<string> ErrorObservable => errorSubject.AsObservable();

        public AppStateService(IWarframeStatAPI api, AppSettings appSettings)
        {
            this.appSettings = appSettings;
            this.api = api;

            worldStateSubject = new BehaviorSubject<WorldState>(null);
            refreshingSubject = new BehaviorSubject<bool>(false);
            errorSubject = new BehaviorSubject<string>(string.Empty);
        }


        public void FetchWorldState()
        {
            refreshingSubject.OnNext(true);

            var plaform = appSettings.Platform;
            var dis = api.FetchWorldState(plaform)
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