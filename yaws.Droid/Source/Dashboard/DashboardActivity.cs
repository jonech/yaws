using System;
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

namespace yaws.Droid.Source.Dashboard
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class DashboardActivity : AppCompatActivity
    {
        public const string CHANNEL_ID = "yaws_notification_channel";
        protected AppStateService AppStateService { get; set; }

        private DashboardPagerAdapter _dashboardPagerAdapter;
        private IDisposable _errorSubscription;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            InitNotificationChannel();

            SetContentView(Resource.Layout.activity_dashboard);

            using (var scope = App.Container.BeginLifetimeScope())
            {
                AppStateService= scope.Resolve<AppStateService>();
            }

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_dashboard);
            SetSupportActionBar(toolbar);


            var viewPager = FindViewById<ViewPager>(Resource.Id.pager_main_dashboard);
            _dashboardPagerAdapter = new DashboardPagerAdapter(
                new List<StatsFragment>()
                {
                    new CommonStatsFragment(),
                    new CetusStatsFragment(),
                    new VallisStatsFragment(),
                    new FissureStatsFragment(),
                    new InvasionStatsFragment()
                },
                SupportFragmentManager);
            viewPager.Adapter = _dashboardPagerAdapter;
            viewPager.OffscreenPageLimit = 4;

            var tab = FindViewById<Widget.TabLayout>(Resource.Id.tab_main_dashboard);
            tab.SetupWithViewPager(viewPager);
        }

        protected override void OnStart()
        {
            base.OnStart();

            _errorSubscription = AppStateService.ErrorObservable
                .Where(error => !string.IsNullOrEmpty(error))
                .RunOnUI()
                .Subscribe(error =>
                {
                    Android.Widget.Toast.MakeText(this, error.ToString(), Android.Widget.ToastLength.Short).Show();
                });

            AppStateService.FetchWorldState();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_errorSubscription != null)
                _errorSubscription.Dispose();
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
            AppStateService.FetchWorldState();
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
            var channel = new NotificationChannel(CHANNEL_ID,
                                          "FCM Notifications",
                                          NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}