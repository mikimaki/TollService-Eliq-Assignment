using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TollFee.Api.Models;
using TollFee.Api.Services;

namespace TollFee.Api.Controllers
{
    using Persistence;

    [ApiController]
    [Route("[controller]")]
    public class TollFeeController : ControllerBase
    {
        private readonly TollDBContext _dbContext;

        public TollFeeController(TollDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("CalculateFee")]
        public CalculateFeeResponse CalculateFee([FromBody] DateTime[] request)
        {
            if (request.Length == 0)
            {
                request = new DateTime[]
                {
                    new(2021, 12, 1, 7, 30, 1),
                    new(2021, 12, 1, 9, 30, 1),
                    new(2021, 1, 1),
                    new(2021, 1, 2)
                };
            }

            var notFree = TollFreeService.RemoveFree(request);
            var totalFee = TollFeeService.GetFee(notFree);

            var response = new CalculateFeeResponse
            {
                TotalFee = totalFee,
                AverageFeePerDay = totalFee / request.Distinct().Count()
            };

            return response;
        }
    }
}