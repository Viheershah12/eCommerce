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
        public Guid CustomerId { get; set; }

        public required string CustomerName { get; set; }

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

        #region Address
        public Address? BillingAddress { get; set; }

        public Address? ShippingAddress { get; set; }

        public class Address
        {
            public required string AddressLine1 { get; set; } 

            public string? AddressLine2 { get; set; } 

            public string? AddressLine3 { get; set; }

            public string? PostCode { get; set; } 

            public string? Town { get; set; } 
            
            public CountryEnum Country { get; set; } = CountryEnum.KE;
            
            public string? TelephoneNumber { get; set; }
            
            public string? FaxNumber { get; set; }
            
            public string? Email { get; set; }
            
            public string? Website { get; set; }
        }
        #endregion 
    }
}
