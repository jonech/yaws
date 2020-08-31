using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Text;
using yaws.Common;

namespace Zaw
{
    public class FCM
    {
        public static Message CreateMessage(string title, string body, YawsNotification.Topic topic, string tag = null, TimeSpan? ttl = null)
        {
#if DEBUG
            var fcmTopic = $"Debug-{Program.Config.WFPlatform}-{topic}";
#else
            var fcmTopic = $"{Program.Config.WFPlatform}-{topic}";
#endif

            return new Message
            {
                Android = new AndroidConfig
                {
                    Notification = new AndroidNotification
                    {
                        Title = title,
                        Body = body,
                        ChannelId = YawsNotification.ChannelId,
                        Tag = tag
                    },
                    TimeToLive = ttl ?? TimeSpan.FromMinutes(45),
                },
                Topic = fcmTopic,
            };
        }

        public static Message CreateMessage(string title, string body, YawsNotification.Topic topic, TimeSpan? ttl = null)
        {
            return CreateMessage(title, body, topic, null, ttl);
        }
    }
}
