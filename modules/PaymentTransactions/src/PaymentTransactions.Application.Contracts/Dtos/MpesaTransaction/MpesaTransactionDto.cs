using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace PaymentTransactions.Dtos.MpesaTransaction
{
    public class GetMpesaTransactionListDto : PagedResultRequestDto
    {
        public string? Sorting { get; set; }
    }

    public class MpesaTransactionDto : BaseIdModel
    {
        public Guid PaymentTransactionId { get; set; }

        public required int ResultCode { get; set; }

        public required string ResultDesc { get; set; }

        public required string MpesaReceiptNumber { get; set; }

        public required string PhoneNumber { get; set; }
    }
}
