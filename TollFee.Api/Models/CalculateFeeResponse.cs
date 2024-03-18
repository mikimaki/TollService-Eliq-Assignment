namespace TollFee.Api.Models
{
    public record CalculateFeeResponse
    {
        public decimal TotalFee { get; init; }
        public decimal AverageFeePerDay { get; init; }
    }
}
