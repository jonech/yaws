using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using yaws.Common;

namespace yaws.Droid.Source.Setting
{
    public class NotificationSetting
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public string SettingName { get; set; }
        public YawsNotification.Topic Topic { get; set; }
    }
}