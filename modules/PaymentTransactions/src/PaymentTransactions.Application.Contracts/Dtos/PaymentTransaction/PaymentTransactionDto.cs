﻿using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace PaymentTransactions.Dtos.PaymentTransaction
{
    public class GetPaymentTransactionListDto : PagedResultRequestDto
    {
        public string? Sorting { get; set; }

        public string? Filter { get; set; }
    }

    public class PaymentTransactionDto : BaseIdModel
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }

        public required string PaymentMethodSystemName { get; set; }

        public PaymentTransactionStatus Status { get; set; } = PaymentTransactionStatus.Pending;

        public DateTime? PaymentDate { get; set; }
    }
}
