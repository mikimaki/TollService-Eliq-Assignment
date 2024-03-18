namespace TollFee.Api.Services;

using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class CongestionTaxRateAggregateFactory : ICongestionTaxRateAggregateFactory
{
    private readonly TollDBContext _tollDbContext;

    public CongestionTaxRateAggregateFactory(TollDBContext tollDbContext)
    {
        _tollDbContext = tollDbContext;
    }

    public async Task<CongestionTaxRateAggregate> CreateCongestionTaxRateAggregate(int[] configYears)
    {
        var tollTaxRates = await _tollDbContext.TaxRates.Where(x => configYears.Contains(x.Year)).ToArrayAsync();
        var tollZeroTaxRates = await _tollDbContext.ZeroTaxRates.Where(x => configYears.Contains(x.Year)).ToArrayAsync();

        AllYearsAreConfigured(tollTaxRates, tollZeroTaxRates, configYears);
        
        var taxRates = await _tollDbContext.TaxRates
            .Select(x => new CongestionTaxRate
            {
                FromMinuteOfTheDay = x.FromMinute,
                ToMinuteOfTheDay = x.ToMinute,
                Price = x.Price
            })
            .ToArrayAsync();

        var zeroTaxRates = await _tollDbContext.ZeroTaxRates
            .Select(x => new CongestionTaxZeroRate
            {
                ZeroTaxRateDate = new DateOnly(x.Date.Year, x.Date.Month, x.Date.Day)
            })
            .ToArrayAsync();

        return new CongestionTaxRateAggregate(taxRates, zeroTaxRates);
    }

    private static void AllYearsAreConfigured(TollTaxRate[] taxRates, TollZeroTaxRate[] zeroTaxRates, int[] configYears)
    {
        var taxRateMissingConfigYears = configYears.Where(x => taxRates.All(y => y.Year != x)).ToArray();
        var zeroTaxRateMissingConfigYears = configYears.Where(x => zeroTaxRates.All(y => y.Year != x)).ToArray();

        if (taxRateMissingConfigYears.Any() || zeroTaxRateMissingConfigYears.Any())
        {
            var message = $"Tax rate config missing for years: {string.Join(",", taxRateMissingConfigYears.Union(zeroTaxRateMissingConfigYears).Distinct())}";

            throw new UnconfiguredTaxRateYearsException(message);
        }
    }

}