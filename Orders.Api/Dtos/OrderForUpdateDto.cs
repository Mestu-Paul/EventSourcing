﻿namespace Orders.Api.Dtos
{
    public class OrderForUpdateDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CustomerEmail { get; set; }
    }
}
