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
using yaws.Droid.Source.Dashboard.Fragments;

namespace yaws.Droid.Source.Dashboard
{
    public class DashboardPagerAdapter : FragmentPagerAdapter, ViewPager.IOnPageChangeListener
    {
        public StatsFragment CurrentFragment { get; private set; }

        private readonly List<StatsFragment> fragments;

        public DashboardPagerAdapter(List<StatsFragment> fragments, FragmentManager fm) : base(fm)
        {
            this.fragments = fragments;
        }

        public override int Count => fragments.Count;


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

        public override void SetPrimaryItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            if (fragments[position] != null)
                CurrentFragment = fragments[position];

            base.SetPrimaryItem(container, position, @object);
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