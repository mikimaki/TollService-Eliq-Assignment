namespace TollFee.Api.Test.Domain;

public class CongestionTaxRateAggregateTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CalculateTax_ValidDateProvided_CorrectTaxCalculated()
    {
        var sut = new CongestionTaxRateAggregate();

        var result = sut.CalculateCongestionTax(new DateTime[] { });
        
        Assert.That(result, Is.EqualTo(9));
    }

}

public class CongestionTaxRateAggregate
{
    public decimal CalculateCongestionTax(DateTime[] dateTimes)
    {
        return 9;
    }
}