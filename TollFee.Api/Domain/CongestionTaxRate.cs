namespace TollFee.Api.Domain;

public record CongestionTaxRate
{
    public int FromMinuteOfTheDay { get; init; }
    public int ToMinuteOfTheDay { get; init; }
    public decimal Price { get; init; }

}