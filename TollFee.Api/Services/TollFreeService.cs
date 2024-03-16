using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFee.Api.Services
{
    public static class TollFreeService
    {
        internal static IEnumerable<DateTime> RemoveFree(DateTime[] passages)
        {
            foreach (var p in passages)
            {
                if (p.DayOfWeek != DayOfWeek.Saturday && p.DayOfWeek != DayOfWeek.Sunday && !OtherFreeDays.Contains(p.Date) && p.Month != 7)
                    yield return p;
            }
        }

        internal static DateTime[] OtherFreeDays = new[]
        {
            new DateTime(2021, 1, 1),
            new DateTime(2021, 1, 5),
            new DateTime(2021, 1, 6),
            new DateTime(2021, 4, 1),
            new DateTime(2021, 4, 2),
            new DateTime(2021, 4, 5),
            new DateTime(2021, 4, 30),
            new DateTime(2021, 5, 12),
            new DateTime(2021, 5, 13),
            new DateTime(2021, 6, 25),
            new DateTime(2021, 11, 5),
            new DateTime(2021, 12, 24),
            new DateTime(2021, 12, 31)
        };
    }
}
