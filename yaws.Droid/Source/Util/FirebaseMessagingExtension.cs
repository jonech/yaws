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
using Firebase.Messaging;
using yaws.Core.Constant;

namespace yaws.Droid.Source.Util
{
    public static class FirebaseMessagingExtension
    {
        public static Android.Gms.Tasks.Task SubscribeToTopic(this FirebaseMessaging fb, string platform, YawsNotification.Topic topic)
        {
            var fbTopic = $"{platform}_{topic}";
            return fb.SubscribeToTopic(fbTopic);
        }

        public static Android.Gms.Tasks.Task UnsubscribeFromTopic(this FirebaseMessaging fb, string platform, YawsNotification.Topic topic)
        {
            var fbTopic = $"{platform}_{topic}";
            return fb.UnsubscribeFromTopic(fbTopic);
        }
    }
}