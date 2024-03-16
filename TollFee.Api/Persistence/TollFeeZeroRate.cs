#nullable disable

namespace TollFee.Api.Persistence
{
    using System;
    using Microsoft.EntityFrameworkCore;

    [Keyless]
    public record TollFeeZeroRate
    {
        public int Year { get; init; }
        public DateTime Date { get; init; }
    }
}
