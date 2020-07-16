using System;
using System.Collections.Generic;
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

        private const string _cetusCycleNotificationKey = "CetusCycleNotificationKey";
        public bool CetusCycleNotification
        {
            get
            {
                return Preferences.Get(_cetusCycleNotificationKey, false);
            }
            set
            {
                Preferences.Set(_cetusCycleNotificationKey, value);
            }
        }
    }
}
