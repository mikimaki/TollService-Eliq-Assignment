namespace TollFee.Api.Models
{
    public record CalculateFeeResponse
    {
        public int TotalFee { get; set; }
        public decimal AverageFeePerDay { get; set; }
    }
}
