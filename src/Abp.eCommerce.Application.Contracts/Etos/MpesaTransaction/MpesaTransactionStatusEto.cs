using Abp.eCommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus;

namespace Abp.eCommerce.Etos.MpesaTransaction
{
    [EventName("MpesaTransactionStatus")]
    public class MpesaTransactionStatusEto
    {
        public PaymentTransactionStatus Status { get; set; }  

        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public string Message { get; set; }
    }
}
