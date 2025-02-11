﻿using EventSourcingApi.Entities;

namespace Orders.Api.Repositories
{
    public interface IWriteOrdersRepository
    {
        Task<Order> CreateOrderAsync(Order entity);
        Task<Order> UpdateOrderAsync(Order entity);
        Task<bool> DeleteOrderAsync(Order entity);
    }
}
