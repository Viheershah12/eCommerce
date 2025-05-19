using Order.Dtos.OrderTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Dtos.Checkout
{
    public class CheckoutDto
    {
        public CheckoutDto(CreateUpdateOrderDto order, CreateUpdatePaymentTransactionDto paymentTransaction)
        {
            Order = order;
            PaymentTransaction = paymentTransaction;
        }

        [Required]
        public CreateUpdateOrderDto Order { get; set; }

        [Required]
        public CreateUpdatePaymentTransactionDto PaymentTransaction { get; set; }
    }

    public class CheckoutResDto 
    {
        public Guid OrderId { get; set; }   

        public Guid PaymentTransactionId { get; set; }
    }
}
