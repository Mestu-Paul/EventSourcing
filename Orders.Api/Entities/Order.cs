namespace EventSourcingApi.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
    }
}
