using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Order.Dtos.OrderTransaction
{
    public class GetOrderListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }

        public OrderStatus? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class OrderDto : BaseIdModel
    {
        public Guid CustomerId { get; set; }

        public DateTime CreationTime { get; set; }

        public required string CustomerName { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.UnPaid;

        public PaymentMethodEnum? PaymentMethod { get; set; }

        public string? PaymentMethodSystemName { get; set; }

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Pending;

        public DateTime? PaidDate { get; set; }

        public DateTime? ShippingDate { get; set; }
    }

    public class CartItemDto
    {
        public Guid CartItemId { get; set; }
        
        public int Quantity { get; set; }
    }
}
