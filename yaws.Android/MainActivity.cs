using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using V4 = Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using yaws.Android.Source.Dashboard;
using yaws.Android.Source.Setting;

namespace yaws.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, ViewPager.IOnPageChangeListener
    {
        BottomNavigationView navigation;
        ViewPager mainPager;
        Toolbar toolbar;

        DashboardFragment dashboardFragment;
        SettingFragment settingFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            dashboardFragment = new DashboardFragment();
            settingFragment = new SettingFragment();

            mainPager = FindViewById<ViewPager>(Resource.Id.pager_main);
            mainPager.Adapter = new MainViewPagerAdapter(
                new List<V4.Fragment>() { dashboardFragment, settingFragment },
                SupportFragmentManager);
            mainPager.AddOnPageChangeListener(this);

            navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(toolbar);
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

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
                    if (dashboardFragment != null)
                        dashboardFragment.OnRefresh();

                    break;
            }
            return true;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_dashboard:
                    mainPager.SetCurrentItem(0, false);
                    return true;
                case Resource.Id.navigation_setting:
                    mainPager.SetCurrentItem(1, false);
                    return true;
            }
            return false;
        }



        public void OnPageScrollStateChanged(int state)
        {
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageSelected(int position)
        {
            navigation.Menu.GetItem(position).SetChecked(true);
        }
    }
}

