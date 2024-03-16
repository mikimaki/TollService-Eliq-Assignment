namespace TollFee.Api.Test;

using Controllers;
using Persistence;

public class TollFeeControllerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CalculateFee_DefaultInputProvided_ReturnsCalculatedFeeAndDailyAverage()
    {
        var sut = new TollFeeController(new TollDBContext());

        var response = sut.CalculateFee(new DateTime[] { });

        Assert.That(response.TotalFee, Is.EqualTo(31));
        Assert.That(response.AverageFeePerDay, Is.EqualTo(7));
    }

    [Test]
    public void CalculateFee_InputProvided_ReturnsCalculatedFeeAndDailyAverage()
    {
        var sut = new TollFeeController(new TollDBContext());

        var response = sut.CalculateFee(new DateTime[]
        {
            new(2021, 12, 1)
        });

        Assert.That(response.TotalFee, Is.EqualTo(31));
        Assert.That(response.AverageFeePerDay, Is.EqualTo(7));
    }
}