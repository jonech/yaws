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
using Android.Support.V7.Widget;

using Autofac;
using Firebase.Messaging;

using WarframeStatService.Constant;
using yaws.Common;
using yaws.Core;
using yaws.Droid.Source.Util;

namespace yaws.Droid.Source.Setting
{
    [Activity(Label = "Setting")]
    public class SettingActivity : AppCompatActivity
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

            InitPlatformSetting();

            InitNotificationSetting();
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


        private void InitPlatformSetting()
        {
            var platformRadioGroup = FindViewById<RadioGroup>(Resource.Id.radio_group_setting_platform);
            platformRadioGroup.Check(GetPlatformSettingId(AppSettings.Platform));
            platformRadioGroup.CheckedChange += PlatfromRadioGroupCheckedChanged;
        }

        private void PlatfromRadioGroupCheckedChanged(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            var prevPlatform = AppSettings.Platform;
            var newPlatform = GetWFPlatformEnum(e.CheckedId);
            AppSettings.Platform = newPlatform;
            ResubscribeNotificationPlatform(prevPlatform, newPlatform);
        }

        private string GetWFPlatformEnum(int platformId)
        {
            switch (platformId)
            {
                case Resource.Id.radio_button_setting_platform_pc:
                    return WFPlatform.PC;
                case Resource.Id.radio_button_setting_platform_ps4:
                    return WFPlatform.PS4;
                case Resource.Id.radio_button_setting_platform_xb1:
                    return WFPlatform.Xbox;
                case Resource.Id.radio_button_setting_platform_switch:
                    return WFPlatform.Switch;
                default:
                    return WFPlatform.PC;
            }
        }

        private int GetPlatformSettingId(string platform)
        {
            switch (platform)
            {
                case WFPlatform.PC:
                    return Resource.Id.radio_button_setting_platform_pc;
                case WFPlatform.PS4:
                    return Resource.Id.radio_button_setting_platform_ps4;
                case WFPlatform.Xbox:
                    return Resource.Id.radio_button_setting_platform_xb1;
                case WFPlatform.Switch:
                    return Resource.Id.radio_button_setting_platform_switch;
                default:
                    return Resource.Id.radio_button_setting_platform_pc;
            }
        }

        private void ResubscribeNotificationPlatform(string previousPlatform, string newPlatform)
        {
            foreach (var topic in YawsNotification.AvailableTopics)
            {
                if (AppSettings.GetSwitchNotificationSetting(topic))
                {
                    FirebaseMessaging.Instance.UnsubscribeFromTopic(previousPlatform, topic);
                    FirebaseMessaging.Instance.SubscribeToTopic(newPlatform, topic);
                }
            }
        }


        private void InitNotificationSetting()
        {
            var notificationRecycler = FindViewById<RecyclerView>(Resource.Id.recyler_notification_settings);

            var notificationSettings = new List<NotificationSetting>();
            foreach (var topic in YawsNotification.AvailableTopics)
            {
                var setting = new NotificationSetting
                {
                    Name = GetNotificationSettingName(topic),
                    Topic = topic,
                    IsChecked = AppSettings.GetSwitchNotificationSetting(topic)
                };
                notificationSettings.Add(setting);
            }

            var adapter = new NotificationRecyclerAdapter(notificationSettings, AppSettings);
            notificationRecycler.SetAdapter(adapter);

            var layoutManager = new LinearLayoutManager(ApplicationContext);
            notificationRecycler.SetLayoutManager(layoutManager);

            var divider = new DividerItemDecoration(ApplicationContext, layoutManager.Orientation);
            notificationRecycler.AddItemDecoration(divider);
        }

        private string GetNotificationSettingName(YawsNotification.Topic topic)
        {
            switch (topic)
            {
                case YawsNotification.Topic.Arbitration:
                    return Resources.GetString(Resource.String.arbitration);
                case YawsNotification.Topic.SentientOutpost:
                    return Resources.GetString(Resource.String.sentient_outpost);
                case YawsNotification.Topic.EarthCycle:
                    return Resources.GetString(Resource.String.earth_cycle);
                case YawsNotification.Topic.CetusBounty:
                    return Resources.GetString(Resource.String.cetus_bounty);
                case YawsNotification.Topic.CetusCycle:
                    return Resources.GetString(Resource.String.cetus_cycle);
                case YawsNotification.Topic.VallisBounty:
                    return Resources.GetString(Resource.String.vallis_bounty);
                case YawsNotification.Topic.VallisCycle:
                    return Resources.GetString(Resource.String.vallis_cycle);
                case YawsNotification.Topic.Invasion:
                    return Resources.GetString(Resource.String.invasion);
                case YawsNotification.Topic.FissureLith:
                    return Resources.GetString(Resource.String.fissure_lith);
                case YawsNotification.Topic.FissureMeso:
                    return Resources.GetString(Resource.String.fissure_meso);
                case YawsNotification.Topic.FissureNeo:
                    return Resources.GetString(Resource.String.fissure_neo);
                case YawsNotification.Topic.FissureAxi:
                    return Resources.GetString(Resource.String.fissure_axi);
                case YawsNotification.Topic.FissureRequiem:
                    return Resources.GetString(Resource.String.fissure_requiem);
                default:
                    return string.Empty;
            }
        }
    }
}