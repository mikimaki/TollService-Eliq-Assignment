namespace TollFee.Api.Test.Controllers;

using Api.Controllers;
using Api.Domain;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;

public class TollFeeControllerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CalculateFee_DefaultInputProvided_ReturnsCalculatedFeeAndDailyAverage()
    {
        var aggregateFactoryMock = new Mock<ICongestionTaxRateAggregateFactory>();
        aggregateFactoryMock
            .Setup(x => x.CreateCongestionTaxRateAggregate(It.IsAny<int[]>()))
            .ReturnsAsync(CreateTestAggregate());

        var sut = new TollFeeController(aggregateFactoryMock.Object);

        var response = await sut.CalculateFee(new DateTime[]
        {
            new(2021, 12, 1, 7, 30, 1),
            new(2021, 12, 1, 9, 30, 1),
            new(2021, 1, 1),
            new(2021, 1, 2)
        }) as OkObjectResult;

        Assert.NotNull(response);
        Assert.That(response.StatusCode, Is.EqualTo(200));
        Assert.NotNull(response.Value);
        Assert.IsInstanceOf(typeof(CalculateFeeResponse), response.Value);
        var responseObject = response.Value as CalculateFeeResponse;
        Assert.That(responseObject.TotalFee, Is.EqualTo(31));
        Assert.That(responseObject.AverageFeePerDay, Is.EqualTo(7.75));
    }

    private static CongestionTaxRateAggregate CreateTestAggregate()
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
            }
        };

        var congestionTaxZeroFeeRates = new[]
        {
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2021, 1, 1)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2021, 1, 6)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2021, 3, 29)
            },
            new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(2021, 3, 31)
            }
        };

        return new CongestionTaxRateAggregate(congestionTaxFeeRates, congestionTaxZeroFeeRates);
    }
}