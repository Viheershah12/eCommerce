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

        public required string PaymentMethodSystemName { get; set; }

        public PaymentTransactionStatus Status { get; set; } = PaymentTransactionStatus.Pending;

        public string? GatewayTransactionId { get; set; } // e.g., Mpesa CheckoutRequestID

        public DateTime? PaymentDate { get; set; }

        public string? Notes { get; set; }
    }
}
