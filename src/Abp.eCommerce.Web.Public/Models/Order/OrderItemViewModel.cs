using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;

namespace Abp.eCommerce.Web.Public.Models.Order
{
    public class OrderItemViewModel : BaseIdModel
    {
        public Guid CustomerId { get; set; }

        public DateTime CreationTime { get; set; }

        public required string CustomerName { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.UnPaid;

        public PaymentMethodEnum? PaymentMethod { get; set; }

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Pending;

        public DateTime? PaidDate { get; set; }

        public DateTime? ShippingDate { get; set; }
    }

    public class OrderPaginationModel : BasePaginationModel
    {
        public OrderStatus? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class OrderFilterModel
    {
        public OrderStatus? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
