using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace OrdersMicroservice.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderResponse?>> Get()
        {
            List<OrderResponse?> orders = await _ordersService.GetOrders();
            return orders;
        }

        [HttpGet("/search/orderid/{orderID}")]
        public async Task<OrderResponse?> GetOrderByOrderID(Guid orderID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderID, orderID);
            OrderResponse? order = await _ordersService.GetOrderByCondition(filter);
            return order;
        }

        [HttpGet("/search/productid/{productID}")]
        public async Task<IEnumerable<OrderResponse?>> GetOrderByProductID(Guid productID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.ElemMatch(o => o.OrderItems,
                Builders<OrderItem>.Filter.Eq(oi => oi.ProductID, productID));
            List<OrderResponse?> orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }

        [HttpGet("/search/orderDate/{orderDate}")]
        public async Task<IEnumerable<OrderResponse?>> GetOrderByOrderDate(DateTime orderDate)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderDate.ToString("yyy-MM-dd"), orderDate.ToString("yyy-MM-dd"));
            List<OrderResponse?> orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }

        [HttpGet("/search/userid/{userID}")]
        public async Task<IEnumerable<OrderResponse?>> GetOrderByUserID(Guid userID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.UserID, userID);
            List<OrderResponse?> orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderAddRequest orderAddRequest)
        {
            if (orderAddRequest == null)
            {
                return BadRequest("Order data is null.");
            }

            OrderResponse? orderResponse = await _ordersService.AddOrder(orderAddRequest);
            if (orderResponse == null)
            {
                return Problem("Failed to create order.");
            }

            return Created($"/api/orders/search/orderid/{orderResponse?.OrderID}", orderResponse);
        }

        [HttpPut("{orderID}")]
        public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
            {
                return BadRequest("Order data is null.");
            }

            if (orderID != orderUpdateRequest.OrderID)
            {
                return BadRequest("Order ID in the URL does not match Order ID in the request body.");
            }

            OrderResponse? orderResponse = await _ordersService.UpdateOrder(orderUpdateRequest);
            if (orderResponse == null)
            {
                return Problem("Failed to update order.");
            }

            return Ok(orderResponse);
        }

        [HttpDelete("{orderID}")]
        public async Task<IActionResult> Delete(Guid orderID)
        {
            if (orderID == Guid.Empty)
            {
                return BadRequest("Invalid Order ID.");
            }

            bool isDeleted = await _ordersService.DeleteOrder(orderID);
            if (!isDeleted)
            {
                return NotFound($"Order with ID {orderID} not found.");
            }

            return Ok();
        }
    }
}
