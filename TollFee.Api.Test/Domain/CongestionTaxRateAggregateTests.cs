namespace TollFee.Api.Test.Domain;

using Api.Domain;

public class CongestionTaxRateAggregateTests
{
    private CongestionTaxRateAggregate _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new CongestionTaxRateAggregate(CongestionTaxRateTestData.CongestionTaxFeeRates, CongestionTaxRateTestData.CongestionTaxZeroFeeRates);
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
    public void CalculateTax_TollGatePassedSeveralTimesWithinAnHour_HighestTaxAppliedOnce()
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

    [Test]
    public void CalculateTax_PassagesProvidedSpanningSeveralDays_CalculatesTaxForEachDay()
    {
        var passages = new[]
        {
            new DateTime(2024, 2, 2, 6, 29, 1),
            new DateTime(2024, 2, 2, 7, 35, 1),
            new DateTime(2024, 2, 3, 6, 29, 1),
            new DateTime(2024, 2, 3, 7, 35, 1),
            new DateTime(2024, 2, 5, 7, 35, 1),
        };

        var expectedPrice = 84;
        var result = _sut.CalculateCongestionTax(passages);

        Assert.That(result, Is.EqualTo(expectedPrice));
    }
}