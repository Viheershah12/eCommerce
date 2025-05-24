using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentTransactions.Dtos.PaymentTransaction
{
    public class CreateUpdatePaymentTransactionDto : BaseIdModel
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }

        public string PaymentMethodSystemName => PaymentMethod.ToString();

        public PaymentTransactionStatus Status { get; set; } = PaymentTransactionStatus.Pending;

        public string? GatewayTransactionId { get; set; } // e.g., Mpesa CheckoutRequestID

        public DateTime? PaymentDate { get; set; }

        public string? Notes { get; set; }
    }

    public class OrderPaymentTransactionDto : BaseIdModel
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }

        public string PaymentMethodSystemName => PaymentMethod.ToString();

        public PaymentTransactionStatus Status { get; set; } = PaymentTransactionStatus.Pending;

        public string? GatewayTransactionId { get; set; } // e.g., Mpesa CheckoutRequestID

        public DateTime? PaymentDate { get; set; }

        public string? Notes { get; set; }

        #region Mpesa Transaction
        public MpesaTransactionDto? MpesaTransaction { get; set; }

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

    public class UpdatePaymentTransactionNoteDto : BaseIdModel
    {
        public string Notes { get; set; }
    }
}
