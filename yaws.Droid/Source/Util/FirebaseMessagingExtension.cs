using Firebase.Messaging;
using yaws.Common;

namespace yaws.Droid.Source.Util
{
    public static class FirebaseMessagingExtension
    {
        public static Android.Gms.Tasks.Task SubscribeToTopic(this FirebaseMessaging fb, string platform, YawsNotification.Topic topic)
        {
#if DEBUG
            var fbTopic = $"Debug-{platform}-{topic}";
#else
            var fbTopic = $"{platform}-{topic}";
#endif
            return fb.SubscribeToTopic(fbTopic);
        }

        public static Android.Gms.Tasks.Task UnsubscribeFromTopic(this FirebaseMessaging fb, string platform, YawsNotification.Topic topic)
        {
#if DEBUG
            var fbTopic = $"Debug-{platform}-{topic}";
#else
            var fbTopic = $"{platform}-{topic}";
#endif

            return fb.UnsubscribeFromTopic(fbTopic);
        }
    }
}