﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;

using yaws.Droid.Source.Setting;
using Android.Support.V4.View;
using Widget = Android.Support.Design.Widget;
using yaws.Core;
using yaws.Droid.Source.Dashboard.Fragments;
using Autofac;
using System.Reactive.Threading.Tasks;
using yaws.Droid.Source.Util;
using System.Reactive.Linq;
using Android.Gms.Common;
using yaws.Common;

namespace yaws.Droid.Source.Dashboard
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class DashboardActivity : AppCompatActivity
    {
        public const string CHANNEL_ID = "yaws_notification_channel";

        private AppStateService appStateService;
        private DashboardPagerAdapter dashboardPagerAdapter;
        private IDisposable errorSubscription;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            InitNotificationChannel();

            SetContentView(Resource.Layout.activity_dashboard);

            using (var scope = App.Container.BeginLifetimeScope())
            {
                appStateService= scope.Resolve<AppStateService>();
            }

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_dashboard);
            SetSupportActionBar(toolbar);


            var viewPager = FindViewById<ViewPager>(Resource.Id.pager_main_dashboard);
            dashboardPagerAdapter = new DashboardPagerAdapter(
                new List<StatsFragment>()
                {
                    new CommonStatsFragment(),
                    new CetusStatsFragment(),
                    new VallisStatsFragment(),
                    new FissureStatsFragment(),
                    new InvasionStatsFragment()
                },
                SupportFragmentManager);
            viewPager.Adapter = dashboardPagerAdapter;
            viewPager.OffscreenPageLimit = 4;

            var tab = FindViewById<Widget.TabLayout>(Resource.Id.tab_main_dashboard);
            tab.SetupWithViewPager(viewPager);
        }

        protected override void OnStart()
        {
            base.OnStart();

            errorSubscription = appStateService.ErrorObservable
                .Where(error => !string.IsNullOrEmpty(error))
                .RunOnUI()
                .Subscribe(error =>
                {
                    Android.Widget.Toast.MakeText(this, error.ToString(), Android.Widget.ToastLength.Short).Show();
                });

            appStateService.FetchWorldState();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (errorSubscription != null)
                errorSubscription.Dispose();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_toolbar, menu);
            return true;
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_refresh:
                    Refresh();
                    break;
                case Resource.Id.action_setting:
                    NavigateToSetting();
                    break;
            }
            return true;
        }

        private void NavigateToSetting()
        {
            Intent intent = new Intent(this, typeof(SettingActivity));
            StartActivity(intent);
        }

        private void Refresh()
        {
            appStateService.FetchWorldState();
        }

        private void InitNotificationChannel()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
                return;

            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }
            var channel = new NotificationChannel(YawsNotification.ChannelId,
                                          "World State Alert",
                                          NotificationImportance.Default)
            {

                Description = "Notify when changes occur to World State."
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}