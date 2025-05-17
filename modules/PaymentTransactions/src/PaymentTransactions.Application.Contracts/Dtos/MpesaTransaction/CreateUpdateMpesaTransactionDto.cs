using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentTransactions.Dtos.MpesaTransaction
{
    public class CreateUpdateMpesaTransactionDto : BaseIdModel
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
}
