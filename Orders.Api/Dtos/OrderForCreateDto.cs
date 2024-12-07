namespace Orders.Api.Dtos
{
    public class OrderForCreateDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
    }
}
