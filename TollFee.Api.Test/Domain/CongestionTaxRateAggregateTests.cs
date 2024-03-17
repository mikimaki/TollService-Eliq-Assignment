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
            new CongestionTaxZeroRate()
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
        var result = _sut.CalculateCongestionTax(new DateTime[] { new DateTime(2024, 1, 15, passageHour, passageMinute, 0) });

        Assert.That(result, Is.EqualTo(expectedPrice));
    }

    [Test]
    public void CalculateTax_SeveralValidPassages_CorrectTaxCalculated()
    {
        var passages = new[]
        {
            new DateTime(2024, 2, 2, 6, 30, 1),
            new DateTime(2024, 2, 2, 8, 30, 0),
            new DateTime(2024, 2, 2, 18, 15, 1),
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
        foreach (var tollPassage in tollPassageDateTimes)
        {
            if (_congestionTaxZeroRates.Any(x => x.FreeTaxRateDate.Equals(tollPassage.Date)))
            {
                continue;
            }

            foreach (var taxRate in _congestionTaxRates)
            {
                var tollPassageMinutesOfTheDay = tollPassage.TimeOfDay.TotalMinutes;
                if (tollPassageMinutesOfTheDay >= taxRate.FromMinuteOfTheDay && taxRate.ToMinuteOfTheDay >= tollPassageMinutesOfTheDay)
                {
                    taxSum += taxRate.Price;
                }
            }
        }

        return CapTaxAt60(taxSum);
    }

    private static decimal CapTaxAt60(decimal taxSum)
    {
        return taxSum > 60 ? 60 : taxSum;
    }
}

public record CongestionTaxZeroRate
{
    public DateOnly FreeTaxRateDate { get; init; }
}

public record CongestionTaxRate
{
    public int FromMinuteOfTheDay { get; init; }
    public int ToMinuteOfTheDay { get; init; }
    public decimal Price { get; init; }

}