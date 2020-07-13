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
        private readonly WorldStateRepository _repository;
        private readonly AppSettings _appSettings;

        private readonly BehaviorSubject<WorldStateViewModel> _worldStateSubject;
        public IObservable<WorldStateViewModel> WorldStateObservable => _worldStateSubject.AsObservable();
        public WorldStateViewModel CurrentWorldState => _worldStateSubject.Value;

        private readonly BehaviorSubject<bool> _refreshingSubject;
        public IObservable<bool> RefreshingObservable => _refreshingSubject.AsObservable();

        private readonly BehaviorSubject<string> _errorSubject;
        public IObservable<string> ErrorObservable => _errorSubject.AsObservable();

        public AppStateService(WorldStateRepository repository, AppSettings appSettings)
        {
            _repository = repository;
            _appSettings = appSettings;

            _worldStateSubject = new BehaviorSubject<WorldStateViewModel>(null);
            _refreshingSubject = new BehaviorSubject<bool>(false);
            _errorSubject = new BehaviorSubject<string>(string.Empty);
        }


        public void FetchWorldState()
        {
            _refreshingSubject.OnNext(true);

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
            var plaform = _appSettings.Platform;
            var dis = _repository.GetWorldState(plaform)
                .ToObservable()
                .Subscribe((worldState) =>
                {
                    if (worldState != null)
                        _worldStateSubject.OnNext(worldState);
                },
                err =>
                {
                    _errorSubject.OnNext(err.Message);
                    _refreshingSubject.OnNext(false);
                },
                () =>
                {
                    _refreshingSubject.OnNext(false);
                });
        }
    }
}