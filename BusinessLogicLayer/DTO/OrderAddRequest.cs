using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.DTO
{
   public record OrderAddRequest(Guid userID, DateTime OrderDate, List<OrderItemAddRequest> ordersItems)
    {
        public OrderAddRequest() : this(default, default, default)
        {
        }
    }
}
