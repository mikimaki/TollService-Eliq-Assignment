namespace TollFee.Api.Test.Controllers;

using Api.Controllers;
using Api.Domain;
using Api.Services;
using Domain;
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
    
        [Test]
        public async Task CalculateFee_EmptyInputProvided_ReturnBadRequest()
        {
            var aggregateFactoryMock = new Mock<ICongestionTaxRateAggregateFactory>();
            aggregateFactoryMock
                .Setup(x => x.CreateCongestionTaxRateAggregate(It.IsAny<int[]>()))
                .ReturnsAsync(CreateTestAggregate());
    
            var sut = new TollFeeController(aggregateFactoryMock.Object);
    
            var response = await sut.CalculateFee(new DateTime[]
            {
            }) as BadRequestObjectResult;
    
            Assert.NotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(400));
            var responseMessage = response.Value as string;
            Assert.That(responseMessage, Is.EqualTo("No passages provided in request."));
        }

    private static CongestionTaxRateAggregate CreateTestAggregate()
    {
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

        return new CongestionTaxRateAggregate(CongestionTaxRateTestData.CongestionTaxFeeRates, congestionTaxZeroFeeRates);
    }
}