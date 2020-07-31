using System;

namespace yaws.Common.Extension
{
    public static class TimeSpanExtension
    {
        public static string ToFormattedString(this TimeSpan timeSpan)
        {
            if (timeSpan == null || timeSpan < TimeSpan.Zero)
                return "EXPIRED";

            if (timeSpan.Ticks < TimeSpan.TicksPerMinute)
                return timeSpan.ToString("s's'");
            
            if (timeSpan.Ticks < TimeSpan.TicksPerHour)
                return timeSpan.ToString("m'm 's's'");

            if (timeSpan.Ticks < TimeSpan.TicksPerDay)
                return timeSpan.ToString("h'h 'm'm 's's'");

            return timeSpan.ToString("d'd 'h'h 'm'm 's's'");
        }
    }
}