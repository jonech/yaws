using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V4.Content;
using Android.Views;

namespace yaws.Droid.Source.Setting
{
    [Activity(Label = "Setting")]
    public class SettingActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_setting);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_setting);
            SetSupportActionBar(toolbar);

            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowHomeEnabled(true);
            }
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();

            return base.OnSupportNavigateUp();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.home:
                    OnBackPressed();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}