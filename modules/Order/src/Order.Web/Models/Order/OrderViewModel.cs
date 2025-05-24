using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using System.ComponentModel;
using Abp.eCommerce.Helpers;

namespace Order.Web.Models.Order
{
    public class OrderViewModel : BaseIdModel
    {
        public CustomerDetailViewModel Customer { get; set; }

        public class CustomerDetailViewModel : BaseIdModel
        {
            public string CustomerName { get; set; }

            public UserAddress DeliveryAddress { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public string? HomePhoneNumber { get; set; }

            public Gender? Gender { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public IdentificationType? IdentificationType { get; set; }

            public string? IdentificationNo { get; set; }
        }

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

        #region Order Note
        public List<OrderNoteViewModel> OrderNotes { get; set; } = [];

        public class OrderNoteViewModel : CreationAuditedEntityDto<Guid>
        {
            [Display(Name = "OrderNoteType")]
            [Placeholder("OrderNoteType")]
            [Required]
            public OrderNoteType OrderNoteType { get; set; }

            public string OrderNoteTypeName => OrderNoteType.GetDescription();

            [TextArea(Rows = 3)]
            public string Note { get; set; }

            public string CreatorName { get; set; }
        }
        #endregion

        #region Payment Transaction 
        public PaymentTransactionViewModel? PaymentTransaction { get; set; }

        public MpesaTransactionViewModel? MpesaTransaction { get; set; }

        public class PaymentTransactionViewModel : BaseIdModel
        {
            public Guid OrderId { get; set; }

            public decimal Amount { get; set; }

            public PaymentMethodEnum PaymentMethod { get; set; }

            public required string PaymentMethodSystemName { get; set; }

            public PaymentTransactionStatus Status { get; set; } = PaymentTransactionStatus.Pending;

            public string? GatewayTransactionId { get; set; } // e.g., Mpesa CheckoutRequestID

            public DateTime? PaymentDate { get; set; }

            [Placeholder("Notes")]
            [Display(Name = "Notes")]
            [TextArea(Rows = 3)]
            public string? Notes { get; set; }
        }

        public class MpesaTransactionViewModel : BaseIdModel
        {
            public Guid PaymentTransactionId { get; set; }

            public MpesaTransactionStatusEnum Status { get; set; }

            public DateTime SentOn { get; set; }

            public DateTime? ConfirmedOn { get; set; }

            public required string MerchantRequestId { get; set; }

            public required string CheckoutRequestId { get; set; }

            public required int ResponseCode { get; set; }

            public required string ResponseDecription { get; set; }

            public required string CustomerMessage { get; set; }

            public required int ResultCode { get; set; }

            public required string ResultDesc { get; set; }

            public required string MpesaReceiptNumber { get; set; }

            public required string PhoneNumber { get; set; }
        }
        #endregion 
    }
}
