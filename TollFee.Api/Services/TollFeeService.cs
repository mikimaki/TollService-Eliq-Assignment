using System;
using System.Collections.Generic;

namespace TollFee.Api.Services
{
    public static class TollFeeService
    {
        internal static int GetFee(IEnumerable<DateTime> passages)
        {
            var totalFee = 0;
            foreach (var p in passages)
            {
                if (p.TimeOfDay >= TimeSpan.FromMinutes(6 * 60) && p.TimeOfDay <= TimeSpan.FromMinutes(6 * 60 + 29))
                {
                    totalFee += 9;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(6 * 60 + 30) && p.TimeOfDay <= TimeSpan.FromMinutes(6 * 60 + 59))
                {
                    totalFee += 16;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(7 * 60) && p.TimeOfDay <= TimeSpan.FromMinutes(7 * 60 + 59))
                {
                    totalFee += 22;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(8 * 60) && p.TimeOfDay <= TimeSpan.FromMinutes(8 * 60 + 29))
                {
                    totalFee += 16;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(8 * 60 + 30) && p.TimeOfDay <= TimeSpan.FromMinutes(14 * 60 + 59))
                {
                    totalFee += 9;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(15 * 60) && p.TimeOfDay <= TimeSpan.FromMinutes(15 * 60 + 29))
                {
                    totalFee += 16;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(15 * 60 + 30) && p.TimeOfDay <= TimeSpan.FromMinutes(16 * 60 + 59))
                {
                    totalFee += 22;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(17 * 60) && p.TimeOfDay <= TimeSpan.FromMinutes(17 * 60 + 59))
                {
                    totalFee += 16;
                }
                else if (p.TimeOfDay >= TimeSpan.FromMinutes(18 * 60) && p.TimeOfDay <= TimeSpan.FromMinutes(18 * 60 + 29))
                {
                    totalFee += 9;
                }

            }
            var actualFee = GetActualFee(totalFee);
            return actualFee;
        }

        private static int GetActualFee(int totalFee)
        {
            if (totalFee > 60)
            {
                return 60;
            }
            else
            {
                return totalFee;
            }
        }
    }
}
