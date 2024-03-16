namespace TollFee.Api.Test.Controllers;

using TollFee.Api.Controllers;
using Persistence;

public class TollFeeControllerTests
{
    private TollFeeController _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new TollFeeController(new TollDBContext());
    }

    [Test]
    public void CalculateFee_DefaultInputProvided_ReturnsCalculatedFeeAndDailyAverage()
    {
        var response = _sut.CalculateFee(new DateTime[] { });

        Assert.That(response.TotalFee, Is.EqualTo(31));
        Assert.That(response.AverageFeePerDay, Is.EqualTo(7));
    }

    [Test]
    public void CalculateFee_NonConfiguredYear_ThrowsValidationException()
    {
        var exception = Assert.Throws<Exception>(() =>
        {
            _sut.CalculateFee(new DateTime[]
            {
                new(2021, 12, 1)
            });
        });
    }
}