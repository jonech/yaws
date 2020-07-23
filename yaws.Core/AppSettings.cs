﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using yaws.Core.Constant;

namespace yaws.Core
{
    /// <summary>
    /// Saving and retrieving local app settings using Xamarin.Essentials Preferences
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Key for Platform
        /// </summary>
        private const string _platformKey = "PlatformKey";

        /// <summary>
        /// Get/Set Platform
        /// </summary>
        public string Platform
        {
            get
            {
                return Preferences.Get(_platformKey, WFPlatform.PC);
            }
            set
            {
                Preferences.Set(_platformKey, value);
            }
        }


        public bool GetSwitchNotificationSetting(YawsNotification.Topic notificationTopic)
        {
            if (YawsNotification.AvailableTopics.Contains(notificationTopic))
                return Preferences.Get(nameof(notificationTopic), false);

#if DEBUG
            throw new ArgumentException($"{notificationTopic} is not a valid Notification Topic setting");
#endif
        }

        public void SetSwitchNotificationSetting(YawsNotification.Topic notificationTopic, bool value)
        {
            if (YawsNotification.AvailableTopics.Contains(notificationTopic))
            {
                Preferences.Set(nameof(notificationTopic), value);
                return;
            }

#if DEBUG
            throw new ArgumentException($"{notificationTopic} is not a valid Notification Topic setting");
#endif
        }
    }
}
