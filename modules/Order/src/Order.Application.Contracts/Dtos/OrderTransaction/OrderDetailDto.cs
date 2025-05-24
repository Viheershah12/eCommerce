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
    public class OrderDetailDto : BaseIdModel
    {
        public CustomerDetailDto Customer { get; set; }

        public class CustomerDetailDto : BaseIdModel
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
        public List<OrderItemDto> OrderItems { get; set; } = [];

        public partial class OrderItemDto : BaseIdModel
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
        public List<OrderNoteDto> OrderNotes { get; set; } = [];

        public class OrderNoteDto : CreationAuditedEntityDto<Guid>
        {
            public OrderNoteType OrderNoteType { get; set; }

            public string Note { get; set; }

            public string CreatorName { get; set; }
        }
        #endregion

        #region Payment Transaction 
        public PaymentTransactionDto? PaymentTransaction { get; set; }

        public MpesaTransactionDto? MpesaTransaction { get; set; }

        public class PaymentTransactionDto : BaseIdModel 
        {
            public Guid OrderId { get; set; }

            public decimal Amount { get; set; }

            public PaymentMethodEnum PaymentMethod { get; set; }

            public required string PaymentMethodSystemName { get; set; }

            public PaymentTransactionStatus Status { get; set; } = PaymentTransactionStatus.Pending;

            public string? GatewayTransactionId { get; set; } // e.g., Mpesa CheckoutRequestID

            public DateTime? PaymentDate { get; set; }

            public string? Notes { get; set; }
        }

        public class MpesaTransactionDto : BaseIdModel
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

            public CallbackMetadataDto? Metadata { get; set; }

            public partial class CallbackMetadataDto
            {
                public List<CallbackItemDto> Item { get; set; } = [];

                public partial class CallbackItemDto
                {
                    public string? Name { get; set; }

                    public object? Value { get; set; }
                }
            }
        }
        #endregion 
    }
}
