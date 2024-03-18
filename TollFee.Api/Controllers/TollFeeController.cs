using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TollFee.Api.Models;
using TollFee.Api.Services;

namespace TollFee.Api.Controllers
{
    using System.Threading.Tasks;
    using Persistence;

    [ApiController]
    [Route("[controller]")]
    public class TollFeeController : ControllerBase
    {
        private readonly ICongestionTaxRateAggregateFactory _aggregateFactory;

        public TollFeeController(ICongestionTaxRateAggregateFactory aggregateFactory)
        {
            _aggregateFactory = aggregateFactory;
        }

        [HttpPost("CalculateFee")]
        public async Task<IActionResult> CalculateFee([FromBody] DateTime[] request)
        {
            if (request.Length == 0)
            {
                return BadRequest("No passages provided in request.");
            }

            var aggregate = await _aggregateFactory.CreateCongestionTaxRateAggregate(request
                .DistinctBy(x => x.Year)
                .Select(x => x.Year)
                .ToArray());

            var calculatedFee = aggregate.CalculateCongestionTax(request);

            var response = new CalculateFeeResponse
            {
                TotalFee = calculatedFee,
                AverageFeePerDay = calculatedFee / request.Distinct().Count()
            };

            return Ok(response);
        }
    }
}