using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using yaws.Android.Source.Dashboard;
using yaws.Android.Source.Setting;

namespace yaws.Android
{
    public class MainViewPagerAdapter : FragmentPagerAdapter
    {
        List<Fragment> fragments;
        public MainViewPagerAdapter(List<Fragment> fragments, FragmentManager fm) : base(fm)
        {
            this.fragments = fragments;
        }

        public override int Count => fragments.Count;

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return fragments[0];
                case 1:
                    return fragments[1];
            }

            throw new ArgumentOutOfRangeException($"Attempt to retrieve fragment at position {position}");
        }
    }
}