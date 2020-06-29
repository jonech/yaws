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
using Autofac;

namespace yaws.Droid.Source.Dashboard
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class DashboardActivity : AppCompatActivity, ViewPager.IOnPageChangeListener
    {
        Toolbar toolbar;
        AppStateService worldStateService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_dashboard);

            using (var scope = App.Container.BeginLifetimeScope())
            {
                worldStateService= scope.Resolve<AppStateService>();
            }

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_dashboard);
            SetSupportActionBar(toolbar);


            var dashboardPager = FindViewById<ViewPager>(Resource.Id.pager_main_dashboard);
            var pagerAdapter = new DashboardPagerAdapter(
                new List<StatsFragment>()
                {
                    new CommonStatsFragment(),
                    new CetusStatsFragment(),
                    new VallisStatsFragment(),
                    new FissureStatsFragment(),
                    new InvasionStatsFragment()
                },
                SupportFragmentManager);
            dashboardPager.Adapter = pagerAdapter;
            dashboardPager.AddOnPageChangeListener(pagerAdapter);
            dashboardPager.OffscreenPageLimit = 4;

            var tab = FindViewById<Widget.TabLayout>(Resource.Id.tab_main_dashboard);
            tab.SetupWithViewPager(dashboardPager);
        }

        protected override void OnStart()
        {
            base.OnStart();

            worldStateService.FetchWorldState();
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


        public void OnPageScrollStateChanged(int state)
        {
            //throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
        }
    }
}