using System;
using System.Collections.Generic;

namespace MiniPricer
{
    static class DateTimeExtension
    {
        private static readonly List<DateTime> BankHolydays = new List<DateTime>()
        {
            new DateTime(2015, 1, 1),
            new DateTime(2015, 5, 1),
            new DateTime(2015, 5, 8),
            //...
        };

        public static bool IsOpened(this DateTime source)
        {
            return source.DayOfWeek != DayOfWeek.Saturday &&
                   source.DayOfWeek != DayOfWeek.Sunday && 
                   !BankHolydays.Contains(source);
        }
    }
}
