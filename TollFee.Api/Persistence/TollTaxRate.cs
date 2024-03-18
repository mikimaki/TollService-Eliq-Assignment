#nullable disable

namespace TollFee.Api.Persistence
{
    using Microsoft.EntityFrameworkCore;

    [Keyless]
    public record TollTaxRate
    {
        public int Year { get; init; }
        public int FromMinute { get; init; }
        public int ToMinute { get; init; }
        public decimal Price { get; init; }
    }
}