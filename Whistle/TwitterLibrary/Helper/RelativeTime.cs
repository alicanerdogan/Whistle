using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterLibrary.Helper
{
    public static class RelativeTime
    {
        private const int SECOND = 1;
        private const int MINUTE = 60 * SECOND;
        private const int HOUR = 60 * MINUTE;
        private const int DAY = 24 * HOUR;
        private const int MONTH = 30 * DAY;

        public static string Generate(DateTime dateTime)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "1s" : ts.Seconds + "s";
            }
            if (delta < 2 * MINUTE)
            {
                return "1m";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + "m";
            }
            if (delta < 90 * MINUTE)
            {
                return "1h";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + "1h";
            }
            if (delta < 48 * HOUR)
            {
                return "1d";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + "d";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1M" : months + "M";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1Y" : years + "Y";
            }
        }
    }
}
