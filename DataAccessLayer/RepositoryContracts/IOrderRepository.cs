using DataAccessLayer.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.RepositoryContracts
{
    internal interface IOrderRepository
    {
        Task<IEnumerable<Order?>> GetOrders();

        Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter);

        Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter);

        Task<Order?> AddOrder(Order order);

        Task<Order?> UpdateOrder(Order order);

        Task<bool> DeleteOrder(Order order);

    }
}
