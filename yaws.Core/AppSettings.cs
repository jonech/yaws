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
    /// <remarks>
    /// Android: Xamarin.Essentials is initialised in SettingActivity.
    /// </remarks>
    public class AppSettings
    {
        /// <summary>
        /// Key for Platform
        /// </summary>
        private const string PlatformKey = "PlatformKey";

        /// <summary>
        /// Get/Set Platform
        /// </summary>
        public string Platform
        {
            get
            {
                return Preferences.Get(PlatformKey, WFPlatform.PC);
            }
            set
            {
                Preferences.Set(PlatformKey, value);
            }
        }
    }
}
