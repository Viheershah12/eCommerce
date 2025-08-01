﻿using PaymentTransactions.Dtos.OrderTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PaymentTransactions.Interfaces
{
    public interface IOrderDetailAppService : IApplicationService
    {
        Task<GetOrderPaymentDetailDto> GetOrderPaymentDetailAsync(Guid orderId);
    }
}
