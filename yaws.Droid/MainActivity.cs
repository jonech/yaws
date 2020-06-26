using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using V4App = Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using yaws.Droid.Source.Dashboard;
using yaws.Droid.Source.Setting;
using Widget = Android.Widget;

namespace yaws.Droid
{
    //[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, ViewPager.IOnPageChangeListener
    {
        BottomNavigationView navigation;
        ViewPager mainPager;
        Toolbar toolbar;

        Widget.FrameLayout mainFrame;
        DashboardFragment dashboardFragment;
        SettingFragment settingFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //dashboardFragment = new DashboardFragment();
            //settingFragment = new SettingFragment();

            //mainPager = FindViewById<ViewPager>(Resource.Id.pager_main);
            //mainPager.Adapter = new MainViewPagerAdapter(
            //    new List<V4.Fragment>() { dashboardFragment, settingFragment },
            //    SupportFragmentManager);
            //mainPager.AddOnPageChangeListener(this);

            dashboardFragment = new DashboardFragment();
            settingFragment = new SettingFragment();

            navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(toolbar);

            mainFrame = FindViewById<Widget.FrameLayout>(Resource.Id.frame_main);
            SetFragment(dashboardFragment);
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
                    SetFragment(dashboardFragment);
                    return true;
                case Resource.Id.navigation_setting:
                    SetFragment(settingFragment);
                    return true;
            }
            return false;
        }

        private void SetFragment(V4App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.frame_main, fragment);
            transaction.AddToBackStack(null);
            transaction.Commit();
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

