using Abp.eCommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Dtos.Mpesa
{
    public class MpesaStkPushRequestDto
    {
        public long PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string AccountReference { get; set; }
        public string TransactionDescription { get; set; }
        public Guid PaymentTransactionId { get; set; }  
    }

    public class MpesaStkPushPayload
    {
        public long BusinessShortCode { get; set; }
        public string Password { get; set; }
        public string Timestamp { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public long PartyA { get; set; }
        public long PartyB { get; set; }
        public long PhoneNumber { get; set; }
        public string CallBackURL { get; set; }
        public string AccountReference { get; set; }
        public string TransactionDesc { get; set; }
    }

    public class MpesaStkPushResponse
    {
        public required string MerchantRequestId { get; set; }

        public required string CheckoutRequestId { get; set; }

        public required int ResponseCode { get; set; }

        public required string ResponseDecription { get; set; }

        public required string CustomerMessage { get; set; }
    }

    public class MpesaStkPushQueryResponse
    {
        public int ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public string? MerchantRequestID { get; set; }
        public string? CheckoutRequestID { get; set; }
        public int ResultCode { get; set; }
        public string? ResultDesc { get; set; }
    }

    public class UpdateMpesaStatusDto
    {
        public string CheckoutRequestId { get; set; }
        public string ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public MpesaTransactionStatusEnum Status { get; set; }
    }

    public class MpesaStkPushQueryRequest
    {
        public long BusinessShortCode { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string CheckoutRequestID { get; set; } = string.Empty;
    }
}
