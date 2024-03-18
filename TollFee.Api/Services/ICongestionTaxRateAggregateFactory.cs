namespace TollFee.Api.Services;

using System.Threading.Tasks;
using Domain;

public interface ICongestionTaxRateAggregateFactory
{
    public Task<CongestionTaxRateAggregate> CreateCongestionTaxRateAggregate(int[] configYears);
}