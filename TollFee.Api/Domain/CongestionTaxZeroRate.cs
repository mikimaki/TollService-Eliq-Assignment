namespace TollFee.Api.Domain;

using System;

public record CongestionTaxZeroRate
{
    public DateOnly ZeroTaxRateDate { get; init; }
}