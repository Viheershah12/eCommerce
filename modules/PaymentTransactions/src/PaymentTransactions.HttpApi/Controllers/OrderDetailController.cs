using Microsoft.AspNetCore.Mvc;
using PaymentTransactions.Dtos.OrderTransaction;
using PaymentTransactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace PaymentTransactions.Controllers
{
    [Area(PaymentTransactionsRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = PaymentTransactionsRemoteServiceConsts.RemoteServiceName)]
    [Route("api/paymenttransactions/orderdetail")]
    public class OrderDetailController : PaymentTransactionsController, IOrderDetailAppService
    {
        #region Fields
        private readonly IOrderDetailAppService _orderTransactionAppService;
        #endregion

        #region CTOR
        public OrderDetailController(
            IOrderDetailAppService orderTransactionAppService    
        ) 
        {
            _orderTransactionAppService = orderTransactionAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getOrderPaymentDetail")]
        public async Task<GetOrderPaymentDetailDto> GetOrderPaymentDetailAsync(Guid orderId)
        {
            return await _orderTransactionAppService.GetOrderPaymentDetailAsync(orderId);
        }
    }
}
