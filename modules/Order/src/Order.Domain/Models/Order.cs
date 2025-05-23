using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Order.Models
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public CustomerDetail Customer { get; set; }

        public class CustomerDetail : BaseIdModel
        {
            public string CustomerName { get; set; }

            public UserAddress? DeliveryAddress { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public string? HomePhoneNumber { get; set; }

            public Gender? Gender { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public IdentificationType? IdentificationType { get; set; }

            public string? IdentificationNo { get; set; }
        }

        #region Order Items
        public List<OrderItem> OrderItems { get; set; } = [];

        public partial class OrderItem : BaseIdModel
        {
            public Guid ProductId { get; set; }

            public string ProductName { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }

            public decimal Total => Quantity * Price;
        }
        #endregion 

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.UnPaid;

        public PaymentMethodEnum? PaymentMethod { get; set; }

        public string? PaymentMethodSystemName { get; set; }

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Pending;

        public DateTime? PaidDate { get; set; } 

        public DateTime? ShippingDate { get; set; } 

        public string? Notes { get; set; }
    }
}
