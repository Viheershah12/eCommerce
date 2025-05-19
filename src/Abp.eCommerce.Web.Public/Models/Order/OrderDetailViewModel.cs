using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Abp.eCommerce.Web.Public.Models.Order
{
    public class OrderDetailViewModel : BaseIdModel
    {
        public OrderDetailViewModel(string customerName)
        {
            CustomerName = customerName;
        }

        public Guid CustomerId { get; set; }

        public required string CustomerName { get; set; } = string.Empty;

        #region Order Items
        public List<OrderItemViewModel> OrderItems { get; set; } = [];

        public partial class OrderItemViewModel : BaseIdModel
        {
            public Guid ProductId { get; set; }

            public string ProductName { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }

            public decimal Total => Quantity * Price;
        }
        #endregion 

        public decimal TotalAmount => OrderItems.Sum(x => x.Total);

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.UnPaid;

        public PaymentMethodEnum? PaymentMethod { get; set; }

        public string? PaymentMethodSystemName { get; set; }

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Pending;

        public DateTime? PaidDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public string? Notes { get; set; }

        #region Address
        public AddressViewModel? BillingAddress { get; set; }

        public AddressViewModel? ShippingAddress { get; set; }
        #endregion 
    }

    public class AddressViewModel
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
}
