using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace PaymentTransactions.Models
{
    public class MpesaTransaction : FullAuditedAggregateRoot<Guid>
    {
        public Guid PaymentTransactionId { get; set; }

        public required string MerchantRequestId { get; set; }

        public required string CheckoutRequestId { get; set; }

        public required int ResultCode { get; set; }

        public required string ResultDesc { get; set; }

        public required string MpesaReceiptNumber { get; set; }

        public required string PhoneNumber { get; set; }

        public CallbackMetadata? Metadata { get; set; } 

        public partial class CallbackMetadata
        {
            public List<CallbackItem> Item { get; set; } = [];

            public partial class CallbackItem
            {
                public string? Name { get; set; }

                public object? Value { get; set; }
            }
        }
    }
}
