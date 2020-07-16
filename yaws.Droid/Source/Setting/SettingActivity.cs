using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using yaws.Core.Constant;
using yaws.Core;
using Autofac;
using Android.Support.V7.Widget;
using Firebase.Messaging;
using yaws.Droid.Source.Util;

namespace yaws.Droid.Source.Setting
{
    [Activity(Label = "Setting")]
    public class SettingActivity : AppCompatActivity, RadioGroup.IOnCheckedChangeListener
    {
        protected RadioGroup PlatformRadioGroup;
        protected AppSettings AppSettings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            using (var scope = App.Container.BeginLifetimeScope())
            {
                AppSettings = scope.Resolve<AppSettings>();
            }

            SetContentView(Resource.Layout.activity_setting);

            // initialise toolbar
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_setting);
            SetSupportActionBar(toolbar);
            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetDisplayShowHomeEnabled(true);
            }

            PlatformRadioGroup = FindViewById<RadioGroup>(Resource.Id.radio_group_setting_platform);
            PlatformRadioGroup.Check(GetPlatformSettingId(AppSettings.Platform));
            PlatformRadioGroup.SetOnCheckedChangeListener(this);

            InitCetusNotification();
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

        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            var prevPlatform = AppSettings.Platform;
            var newPlatform = GetWFPlatformEnum(checkedId);
            AppSettings.Platform = newPlatform;
            ResubscribeNotificationPlatform(prevPlatform, newPlatform);
        }

        private void InitCetusNotification()
        {
            var cetusSwitch = FindViewById<SwitchCompat>(Resource.Id.switch_setting_cetus_cycle);
            cetusSwitch.Checked = AppSettings.CetusCycleNotification;
            cetusSwitch.CheckedChange += CetusCycleSwitchCheckedChanged;
        }

        private void CetusCycleSwitchCheckedChanged(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                FirebaseMessaging.Instance.SubscribeToTopic(AppSettings.Platform, WFStat.CetusCycle);
            }
            else
            {
                FirebaseMessaging.Instance.UnsubscribeFromTopic(AppSettings.Platform, WFStat.CetusCycle);
            }

            AppSettings.CetusCycleNotification = e.IsChecked;
        }

        private string GetWFPlatformEnum(int platformId)
        {
            return platformId switch
            {
                Resource.Id.radio_button_setting_platform_pc => WFPlatform.PC,
                Resource.Id.radio_button_setting_platform_ps4 => WFPlatform.PS4,
                Resource.Id.radio_button_setting_platform_xb1 => WFPlatform.Xbox,
                Resource.Id.radio_button_setting_platform_switch => WFPlatform.Switch,
                _ => WFPlatform.PC,
            };
        }

        private int GetPlatformSettingId(string platform)
        {
            return platform switch
            {
                WFPlatform.PC => Resource.Id.radio_button_setting_platform_pc,
                WFPlatform.PS4 => Resource.Id.radio_button_setting_platform_ps4,
                WFPlatform.Xbox => Resource.Id.radio_button_setting_platform_xb1,
                WFPlatform.Switch => Resource.Id.radio_button_setting_platform_switch,
                _ => Resource.Id.radio_button_setting_platform_pc,
            };
        }

        private void ResubscribeNotificationPlatform(string previousPlatform, string newPlatform)
        {
            if (AppSettings.CetusCycleNotification)
            {
                FirebaseMessaging.Instance.UnsubscribeFromTopic(previousPlatform, WFStat.CetusCycle);
                FirebaseMessaging.Instance.SubscribeToTopic(newPlatform, WFStat.CetusCycle);
            }
        }
    }
}