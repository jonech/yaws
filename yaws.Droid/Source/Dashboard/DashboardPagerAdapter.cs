using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Support.V4.View;

using Java.Lang;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;

namespace yaws.Droid.Source.Dashboard
{
    public class DashboardPagerAdapter : FragmentPagerAdapter, ViewPager.IOnPageChangeListener
    {
        List<StatsFragment> fragments;
        AppStateService worldStateService;

        public DashboardPagerAdapter(List<StatsFragment> fragments, FragmentManager fm) : base(fm)
        {
            this.fragments = fragments;

            using (var scope = App.Container.BeginLifetimeScope())
            {
                worldStateService = scope.Resolve<AppStateService>();
            }
        }

        public override int Count => fragments.Count;

        private int previousPosition;

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            var fragment = fragments[position];

            ICharSequence charSeq = new Java.Lang.String(fragment.Title);
            return charSeq;
        }

        public override Fragment GetItem(int position)
        {
            if (position >= 0 && position < Count)
                return fragments[position];

            throw new ArgumentOutOfRangeException($"Attempt to retrieve fragment at position {position}");
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
            var fragment = fragments[position];
            if (fragment != null)
                worldStateService.CurrentDashboardFragment = fragment.Title;
        }
    }
}