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

namespace yaws.Android.Source.Util
{
    public static class TimeSpanExtension
    {
        public static string ToFormattedString(this TimeSpan timeSpan)
        {
            if (timeSpan == null)
                return string.Empty;

            return timeSpan.ToString("h'h 'm'm 's's'");
        }
    }
}