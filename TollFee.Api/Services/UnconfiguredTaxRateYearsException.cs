namespace TollFee.Api.Services;

using System;

public class UnconfiguredTaxRateYearsException : Exception
{
    public UnconfiguredTaxRateYearsException(string message) : base(message)
    {
    }
}