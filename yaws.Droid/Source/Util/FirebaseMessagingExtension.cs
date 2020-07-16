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

namespace yaws.Droid.Source.Util
{
    public static class FirebaseMessagingExtension
    {
        public static Android.Gms.Tasks.Task SubscribeToTopic(this FirebaseMessaging fb, string platform, string stat)
        {
            return fb.SubscribeToTopic($"{platform}_{stat}");
        }

        public static Android.Gms.Tasks.Task UnsubscribeFromTopic(this FirebaseMessaging fb, string platform, string stat)
        {
            return fb.UnsubscribeFromTopic($"{platform}_{stat}");
        }
    }
}