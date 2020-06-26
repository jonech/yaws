using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Autofac;
using ReactiveUI;
using yaws.Core;
using yaws.Core.ViewModel;
using yaws.Droid.Source.Util;

using System.Reactive.Threading.Tasks;
using Android.Support.Design.Widget;

namespace yaws.Droid.Source.Dashboard
{
    public class DashboardFragment : Fragment, ViewPager.IOnPageChangeListener
    {
        ViewPager dashboardPager;

        CommonStatsFragment commonFragment;
        CetusStatsFragment cetusFragment;
        VallisStatsFragment vallisFragment;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.fragment_dashboard, container, false);

            commonFragment = new CommonStatsFragment();
            cetusFragment = new CetusStatsFragment();
            vallisFragment = new VallisStatsFragment();

            dashboardPager = view.FindViewById<ViewPager>(Resource.Id.pager_dashboard);
            var pagerAdapter = new DashboardPagerAdapter(
                new List<StatsFragment>()
                {
                    commonFragment,
                    cetusFragment,
                    vallisFragment
                },
                Activity.SupportFragmentManager);
            dashboardPager.Adapter = pagerAdapter;
            dashboardPager.AddOnPageChangeListener(pagerAdapter);

            var tab = view.FindViewById<TabLayout>(Resource.Id.tab_dashboard);
            tab.SetupWithViewPager(dashboardPager);

            return view;
        }

        //public override void OnStart()
        //{
        //    base.OnStart();
        //}

        //public override void OnDestroy()
        //{
        //    base.OnDestroy();
        //}

        public void OnRefresh()
        {
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
            //throw new NotImplementedException();
        }
    }
}