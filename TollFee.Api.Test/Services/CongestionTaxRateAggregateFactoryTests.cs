namespace TollFee.Api.Test.Services;

using Api.Domain;
using Api.Services;
using AutoFixture;
using Moq;
using Moq.EntityFrameworkCore;
using Persistence;

public class CongestionTaxRateAggregateFactoryTests
{
    private IFixture _fixture;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
    }

    [Test]
    public async Task CreateCongestionTaxRateAggregate_SingleYearProvided_AggregateWithCorrectConfigCreated()
    {
        var expectedTaxRate = _fixture.Create<TollTaxRate>();
        var taxRates = _fixture.CreateMany<TollTaxRate>().ToList();
        taxRates.Add(expectedTaxRate);

        var expectedZeroTaxRate = _fixture.Create<TollZeroTaxRate>();
        expectedZeroTaxRate = expectedZeroTaxRate with { Year = expectedTaxRate.Year };
        var zeroTaxRates = _fixture.CreateMany<TollZeroTaxRate>().ToList();
        zeroTaxRates.Add(expectedZeroTaxRate);

        var dbContextMock = new Mock<TollDBContext>();
        dbContextMock.Setup(x => x.TaxRates).ReturnsDbSet(taxRates);
        dbContextMock.Setup(x => x.ZeroTaxRates).ReturnsDbSet(zeroTaxRates);

        var sut = new CongestionTaxRateAggregateFactory(dbContextMock.Object);

        var result = await sut.CreateCongestionTaxRateAggregate(new[] { expectedTaxRate.Year });

        var expectedDomainTaxRate = new CongestionTaxRate
        {
            FromMinuteOfTheDay = expectedTaxRate.FromMinute,
            ToMinuteOfTheDay = expectedTaxRate.ToMinute,
            Price = expectedTaxRate.Price
        };

        var expectedDomainZeroTaxRate = new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(expectedZeroTaxRate.Date.Year, expectedZeroTaxRate.Date.Month, expectedZeroTaxRate.Date.Day)
        };

        Assert.Contains(expectedDomainTaxRate, result.CongestionTaxRates);
        Assert.Contains(expectedDomainZeroTaxRate, result.CongestionTaxZeroRates);
    }

    [Test]
    public void CreateCongestionTaxRateAggregate_BothTaxRatesAreUnconfiguredForProvidedYear_ExceptionIsThrown()
    {
        var taxRates = _fixture.CreateMany<TollTaxRate>().ToList();
        var zeroTaxRates = _fixture.CreateMany<TollZeroTaxRate>().ToList();

        var dbContextMock = new Mock<TollDBContext>();
        dbContextMock.Setup(x => x.TaxRates).ReturnsDbSet(taxRates);
        dbContextMock.Setup(x => x.ZeroTaxRates).ReturnsDbSet(zeroTaxRates);

        var sut = new CongestionTaxRateAggregateFactory(dbContextMock.Object);

        var unconfiguredYear = _fixture.Create<int>();
        var exception = Assert.ThrowsAsync<UnconfiguredTaxRateYearsException>(() => sut.CreateCongestionTaxRateAggregate(new[] { unconfiguredYear }));
        Assert.AreEqual($"Tax rate config missing for years: {unconfiguredYear}", exception.Message);
    }

    [Test]
    public void CreateCongestionTaxRateAggregate_ZeroTaxRatesUnconfiguredForProvidedYear_ExceptionIsThrown()
    {
        var unconfiguredYear = _fixture.Create<int>();

        var taxRates = _fixture.CreateMany<TollTaxRate>().ToList();
        taxRates = taxRates.Select(x => x with { Year = unconfiguredYear }).ToList();

        var zeroTaxRates = _fixture.CreateMany<TollZeroTaxRate>().ToList();

        var dbContextMock = new Mock<TollDBContext>();
        dbContextMock.Setup(x => x.TaxRates).ReturnsDbSet(taxRates);
        dbContextMock.Setup(x => x.ZeroTaxRates).ReturnsDbSet(zeroTaxRates);

        var sut = new CongestionTaxRateAggregateFactory(dbContextMock.Object);

        var exception = Assert.ThrowsAsync<UnconfiguredTaxRateYearsException>(() => sut.CreateCongestionTaxRateAggregate(new[] { unconfiguredYear }));
        Assert.AreEqual($"Tax rate config missing for years: {unconfiguredYear}", exception.Message);
    }

    [Test]
    public void CreateCongestionTaxRateAggregate_TaxRatesUnconfiguredForProvidedYear_ExceptionIsThrown()
    {
        var unconfiguredYear = _fixture.Create<int>();

        var taxRates = _fixture.CreateMany<TollTaxRate>().ToList();

        var zeroTaxRates = _fixture.CreateMany<TollZeroTaxRate>().ToList();
        zeroTaxRates = zeroTaxRates.Select(x => x with { Year = unconfiguredYear }).ToList();

        var dbContextMock = new Mock<TollDBContext>();
        dbContextMock.Setup(x => x.TaxRates).ReturnsDbSet(taxRates);
        dbContextMock.Setup(x => x.ZeroTaxRates).ReturnsDbSet(zeroTaxRates);

        var sut = new CongestionTaxRateAggregateFactory(dbContextMock.Object);

        var exception = Assert.ThrowsAsync<UnconfiguredTaxRateYearsException>(() => sut.CreateCongestionTaxRateAggregate(new[] { unconfiguredYear }));
        Assert.AreEqual($"Tax rate config missing for years: {unconfiguredYear}", exception.Message);
    }

}