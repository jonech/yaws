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
        public static Message CreateMessage(string title, string body, YawsNotification.Topic topic, TimeSpan? ttl = null)
        {
            return new Message
            {
                Android = new AndroidConfig
                {
                    Notification = new AndroidNotification
                    {
                        Title = title,
                        Body = body,
                    },
                    TimeToLive = ttl ?? TimeSpan.FromMinutes(45)
                },
                Topic = $"{Program.Config.WFPlatform}-{topic}"
            };
        }
    }
}
