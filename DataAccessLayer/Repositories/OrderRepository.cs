using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _ordersCollection;
        private readonly string collectionName = "orders";


        public OrderRepository(IMongoDatabase database) 
        {
            _ordersCollection = database.GetCollection<Order>(collectionName);
        }

        public async Task<Order?> AddOrder(Order order)
        {
            order.OrderID = Guid.NewGuid();

            await _ordersCollection.InsertOneAsync(order);
            return order;
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderID, order.OrderID);

            Order? existingOrder = (await _ordersCollection.FindAsync(filter)).FirstOrDefault();
            if (existingOrder == null)
            {
                return false;
            }

            DeleteResult deleteResult = await _ordersCollection.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;
        }

        public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
        {
            return (await _ordersCollection.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<IEnumerable<Order?>> GetOrders()
        {
            return (await _ordersCollection.FindAsync(Builders<Order>.Filter.Empty)).ToList();
        }

        public async Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter)
        {
            return (await _ordersCollection.FindAsync(filter)).ToList();
        }

        public async Task<Order?> UpdateOrder(Order order)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderID, order.OrderID);

            Order? existingOrder = (await _ordersCollection.FindAsync(filter)).FirstOrDefault();
            if (existingOrder == null)
            {
                return null;
            }

            ReplaceOneResult replaceOneResult = await _ordersCollection.ReplaceOneAsync(filter, order);
            return order;
        }
    }
}
