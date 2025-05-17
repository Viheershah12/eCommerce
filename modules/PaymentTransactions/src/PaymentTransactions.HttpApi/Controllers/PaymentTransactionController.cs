using Microsoft.AspNetCore.Mvc;
using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using PaymentTransactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace PaymentTransactions.Controllers
{
    [Area(PaymentTransactionsRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = PaymentTransactionsRemoteServiceConsts.RemoteServiceName)]
    [Route("api/paymenttransactions/paymenttransaction")]
    public class PaymentTransactionController : PaymentTransactionsController, IPaymentTransactionAppService
    {
        #region Fields
        private readonly IPaymentTransactionAppService _paymentTransactionAppService;
        #endregion

        #region CTOR
        public PaymentTransactionController(
            IPaymentTransactionAppService paymentTransactionAppService    
        ) 
        {
            _paymentTransactionAppService = paymentTransactionAppService;
        }
        #endregion 

        [HttpGet]
        [Route("status")]
        public async Task<int> GetStatusAsync(Guid transactionId)
        {
            return await _paymentTransactionAppService.GetStatusAsync(transactionId);
        }

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<PaymentTransactionDto>> GetListAsync(GetPaymentTransactionListDto dto)
        {
            return await _paymentTransactionAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdatePaymentTransactionDto dto)
        {
            return await _paymentTransactionAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdatePaymentTransactionDto> GetAsync(Guid id)
        {
            return await _paymentTransactionAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdatePaymentTransactionDto dto)
        {
            await _paymentTransactionAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _paymentTransactionAppService.DeleteAsync(id);
        }
    }
}
