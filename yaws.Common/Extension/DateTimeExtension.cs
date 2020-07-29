using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace yaws.Common.Extension
{
    public static class DateTimeExtension
    {
        public static bool IsExpired(this DateTime datetime)
        {
            if (datetime == null)
                return true;

            return DateTime.UtcNow > datetime;
        }
    }
}
