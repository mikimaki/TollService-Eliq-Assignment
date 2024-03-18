namespace TollFee.Api.Domain;

using System;
using System.Collections.Generic;
using System.Linq;

public class CongestionTaxRateAggregate
{
    public readonly CongestionTaxRate[] CongestionTaxRates;
    public readonly CongestionTaxZeroRate[] CongestionTaxZeroRates;

    public CongestionTaxRateAggregate(CongestionTaxRate[] congestionTaxRates, CongestionTaxZeroRate[] congestionTaxZeroRates)
    {
        CongestionTaxRates = congestionTaxRates;
        CongestionTaxZeroRates = congestionTaxZeroRates;
    }

    public decimal CalculateCongestionTax(DateTime[] tollPassageDateTimes)
    {
        var taxSum = 0m;
        var lastPassagePlusOneHour = DateTime.MinValue;
        var passageTaxesWithinOneHour = new List<decimal> { 0 };

        foreach (var tollPassage in tollPassageDateTimes.OrderBy(x => x))
        {
            if (CongestionTaxZeroRates.Any(x => DatesEqual(x.ZeroTaxRateDate, tollPassage.Date)))
            {
                continue;
            }

            if (!PassageIsWithinOneHourOfThePreviousPassages(tollPassage, lastPassagePlusOneHour))
            {
                taxSum += passageTaxesWithinOneHour.Max();
                passageTaxesWithinOneHour = new List<decimal> { 0 };
                lastPassagePlusOneHour = tollPassage.AddHours(1);
            }

            foreach (var taxRate in CongestionTaxRates)
            {
                var tollPassageMinutesOfTheDay = MinutesOfDayIgnoreSecondsAndMilliseconds(tollPassage.TimeOfDay);
                if (tollPassageMinutesOfTheDay >= taxRate.FromMinuteOfTheDay && taxRate.ToMinuteOfTheDay >= tollPassageMinutesOfTheDay)
                {
                    passageTaxesWithinOneHour.Add(taxRate.Price);
                }
            }
        }

        taxSum += passageTaxesWithinOneHour.Max();

        return CapTaxAt60(taxSum);
    }

    private static bool PassageIsWithinOneHourOfThePreviousPassages(DateTime tollPassage, DateTime lastPassagePlusOneHour) => DateTime.Compare(tollPassage, lastPassagePlusOneHour) == -1;

    private static double MinutesOfDayIgnoreSecondsAndMilliseconds(TimeSpan tollPassageTimeOfDay)
    {
        var sanitizedTimeSpan = new TimeSpan(tollPassageTimeOfDay.Hours, tollPassageTimeOfDay.Minutes, 0);
        return sanitizedTimeSpan.TotalMinutes;
    }

    private static bool DatesEqual(DateOnly dateOnly, DateTime dateTime)
    {
        return dateOnly.Year == dateTime.Year
               && dateOnly.Month == dateTime.Month
               && dateOnly.Day == dateTime.Day;
    }

    private static decimal CapTaxAt60(decimal taxSum)
    {
        return taxSum > 60 ? 60 : taxSum;
    }
}