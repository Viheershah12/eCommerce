using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentTransactions.Dtos.OrderTransaction
{
    public class GetOrderPaymentDetailDto
    {
        public PaymentTransactionDto? PaymentTransaction { get; set; }

        public MpesaTransactionDto? MpesaTransaction { get; set; } 
    }    
}
