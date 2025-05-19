using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;

namespace Abp.eCommerce.Web.Public.Models.Order
{
    public class PaymentDetailViewModel
    {
        public PaymentTransactionViewModel? PaymentTransaction { get; set; }

        public MpesaTransactionViewModel? MpesaTransaction { get; set; }
    }

    public class PaymentTransactionViewModel : BaseIdModel
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

    public class MpesaTransactionViewModel : BaseIdModel
    {
        public Guid PaymentTransactionId { get; set; }

        public MpesaTransactionStatusEnum Status { get; set; }

        public DateTime SentOn { get; set; }

        public DateTime? ConfirmedOn { get; set; }

        public string? MerchantRequestId { get; set; }

        public string? CheckoutRequestId { get; set; }

        public int? ResponseCode { get; set; }

        public string? ResponseDecription { get; set; }

        public string? CustomerMessage { get; set; }

        public int? ResultCode { get; set; }

        public string? ResultDesc { get; set; }

        public string? MpesaReceiptNumber { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
