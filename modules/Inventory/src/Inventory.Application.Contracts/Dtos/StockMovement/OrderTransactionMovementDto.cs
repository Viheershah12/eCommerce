using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Dtos.StockMovement
{
    public class OrderTransactionMovementDto
    {
        public Guid OrderId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = [];

        public class OrderItemDto
        {
            public Guid ProductId { get; set; }

            public string ProductName { get; set; }

            public decimal Quantity { get; set; }
        }
    }
}
