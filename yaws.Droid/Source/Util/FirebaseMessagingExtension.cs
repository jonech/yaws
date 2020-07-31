using Firebase.Messaging;
using yaws.Common;

namespace yaws.Droid.Source.Util
{
    public static class FirebaseMessagingExtension
    {
        public static Android.Gms.Tasks.Task SubscribeToTopic(this FirebaseMessaging fb, string platform, YawsNotification.Topic topic)
        {
            var fbTopic = $"{platform}-{topic}";
            return fb.SubscribeToTopic(fbTopic);
        }

        public static Android.Gms.Tasks.Task UnsubscribeFromTopic(this FirebaseMessaging fb, string platform, YawsNotification.Topic topic)
        {
            var fbTopic = $"{platform}-{topic}";
            return fb.UnsubscribeFromTopic(fbTopic);
        }
    }
}