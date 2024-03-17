namespace TollFee.Api.Test.Domain;

public class CongestionTaxRateAggregateTests
{
    private CongestionTaxRateAggregate _sut;

    [SetUp]
    public void Setup()
    {
        var congestionTaxFeeRates = new[]
        {
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 360,
                ToMinuteOfTheDay = 389,
                Price = 9
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 390,
                ToMinuteOfTheDay = 419,
                Price = 16
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 420,
                ToMinuteOfTheDay = 479,
                Price = 22
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 480,
                ToMinuteOfTheDay = 509,
                Price = 16
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 510,
                ToMinuteOfTheDay = 899,
                Price = 9
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 900,
                ToMinuteOfTheDay = 929,
                Price = 16
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 930,
                ToMinuteOfTheDay = 1019,
                Price = 22
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 1020,
                ToMinuteOfTheDay = 1079,
                Price = 16
            },
            new CongestionTaxRate
            {
                FromMinuteOfTheDay = 1080,
                ToMinuteOfTheDay = 1109,
                Price = 9
            }
        };

        var congestionTaxZeroFeeRates = new[]
        {
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 1, 1)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 1, 6)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 3, 29)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 3, 31)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 4, 1)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 5, 1)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 5, 9)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 6, 6)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 6, 22)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 10, 2)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 12, 25)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2024, 12, 26)
            }
        };

        _sut = new CongestionTaxRateAggregate(congestionTaxFeeRates, congestionTaxZeroFeeRates);
    }

    [Test]
    [TestCase(5, 25, 0)]
    [TestCase(6, 17, 9)]
    [TestCase(6, 44, 16)]
    [TestCase(7, 29, 22)]
    [TestCase(8, 15, 16)]
    [TestCase(12, 44, 9)]
    [TestCase(15, 7, 16)]
    [TestCase(16, 25, 22)]
    [TestCase(17, 3, 16)]
    [TestCase(18, 15, 9)]
    [TestCase(18, 30, 0)]
    [TestCase(20, 44, 0)]
    [TestCase(2, 44, 0)]
    [TestCase(5, 59, 0)]
    public void CalculateTax_ValidSinglePassage_CorrectTaxCalculated(int passageHour, int passageMinute, decimal expectedPrice)
    {
        var passage = new[] { new DateTime(2024, 1, 15, passageHour, passageMinute, 0) };
        var result = _sut.CalculateCongestionTax(passage);

        Assert.That(result, Is.EqualTo(expectedPrice));
    }

    [Test]
    public void CalculateTax_SeveralValidPassages_CorrectTaxCalculated()
    {
        var passages = new[]
        {
            new DateTime(2024, 2, 2, 8, 30, 0),
            new DateTime(2024, 2, 2, 18, 15, 1),
            new DateTime(2024, 2, 2, 6, 30, 1),
        };

        var expectedPrice = 34;
        var result = _sut.CalculateCongestionTax(passages);

        Assert.That(result, Is.EqualTo(expectedPrice));
    }

    [Test]
    public void CalculateTax_PassageTaxSumOver60_TaxCappedAt60()
    {
        var passages = new[]
        {
            new DateTime(2024, 2, 2, 6, 30, 1),
            new DateTime(2024, 2, 2, 8, 30, 0),
            new DateTime(2024, 2, 2, 7, 30, 0),
            new DateTime(2024, 2, 2, 15, 59, 0),
            new DateTime(2024, 2, 2, 18, 15, 1),
        };

        var expectedPrice = 60;
        var result = _sut.CalculateCongestionTax(passages);

        Assert.That(result, Is.EqualTo(expectedPrice));
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(1, 6)]
    [TestCase(3, 29)]
    [TestCase(3, 31)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(5, 9)]
    [TestCase(6, 6)]
    [TestCase(6, 22)]
    [TestCase(10, 2)]
    [TestCase(12, 25)]
    [TestCase(12, 26)]
    public void CalculateTax_PassagesOccurredOnZeroRateDays_TaxIsZero(int passageMonth, int passageDay)
    {
        var passage = new[] { new DateTime(2024, passageMonth, passageDay, 15, 25, 0) };

        var result = _sut.CalculateCongestionTax(passage);

        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CalculateTax_TollGatePassedSeveralTimesInAnHour_TaxAppliedOnce()
    {
        var passages = new[]
        {
            new DateTime(2024, 2, 2, 6, 29, 1),
            new DateTime(2024, 2, 2, 6, 35, 0),
            new DateTime(2024, 2, 2, 7, 15, 1),
        };

        var expectedPrice = 22;
        var result = _sut.CalculateCongestionTax(passages);

        Assert.That(result, Is.EqualTo(expectedPrice));
    }
}

public class CongestionTaxRateAggregate
{
    private readonly CongestionTaxRate[] _congestionTaxRates;
    private readonly CongestionTaxZeroRate[] _congestionTaxZeroRates;

    public CongestionTaxRateAggregate(CongestionTaxRate[] congestionTaxRates, CongestionTaxZeroRate[] congestionTaxZeroRates)
    {
        _congestionTaxRates = congestionTaxRates;
        _congestionTaxZeroRates = congestionTaxZeroRates;
    }

    public decimal CalculateCongestionTax(DateTime[] tollPassageDateTimes)
    {
        var taxSum = 0m;
        var lastPassagePlusOneHour = DateTime.MinValue;
        var passageTaxesWithinOneHour = new List<decimal> {0};
        foreach (var tollPassage in tollPassageDateTimes.OrderBy(x => x))
        {
            if (_congestionTaxZeroRates.Any(x => DatesEqual(x.ZeroTaxRateDate, tollPassage.Date)))
            {
                continue;
            }

            if (DateTime.Compare(tollPassage, lastPassagePlusOneHour) == 1)
            {
                taxSum += passageTaxesWithinOneHour.Max();
                passageTaxesWithinOneHour = new List<decimal> {0};
                lastPassagePlusOneHour = tollPassage.AddHours(1);
            }

            foreach (var taxRate in _congestionTaxRates)
            {
                var tollPassageMinutesOfTheDay = TimeOfDayIgnoreSecondsAndMilliseconds(tollPassage.TimeOfDay);
                if (tollPassageMinutesOfTheDay >= taxRate.FromMinuteOfTheDay && taxRate.ToMinuteOfTheDay >= tollPassageMinutesOfTheDay)
                {
                    passageTaxesWithinOneHour.Add(taxRate.Price);
                }
            }
        }

        taxSum += passageTaxesWithinOneHour.Max();

        return CapTaxAt60(taxSum);
    }

    private static double TimeOfDayIgnoreSecondsAndMilliseconds(TimeSpan tollPassageTimeOfDay)
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

public record CongestionTaxZeroRate
{
    public DateOnly ZeroTaxRateDate { get; init; }
}

public record CongestionTaxRate
{
    public int FromMinuteOfTheDay { get; init; }
    public int ToMinuteOfTheDay { get; init; }
    public decimal Price { get; init; }

}