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

        public required string MerchantRequestId { get; set; }

        public required string CheckoutRequestId { get; set; }

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
}
