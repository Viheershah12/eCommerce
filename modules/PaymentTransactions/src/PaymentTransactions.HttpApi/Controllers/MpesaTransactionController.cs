using Microsoft.AspNetCore.Mvc;
using PaymentTransactions.Dtos.MpesaTransaction;
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
    [Route("api/paymenttransactions/mpesatransaction")]
    public class MpesaTransactionController : PaymentTransactionsController, IMpesaTransactionAppService
    {
        #region Fields
        private readonly IMpesaTransactionAppService _mpesaTransactionAppService;
        #endregion

        #region CTOR
        public MpesaTransactionController(IMpesaTransactionAppService mpesaTransactionAppService)
        {
            _mpesaTransactionAppService = mpesaTransactionAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<MpesaTransactionDto>> GetListAsync(GetMpesaTransactionListDto dto)
        {
            return await _mpesaTransactionAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateMpesaTransactionDto dto)
        {
            return await _mpesaTransactionAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateMpesaTransactionDto> GetAsync(Guid id)
        {
            return await _mpesaTransactionAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("getByCheckoutRequestId")]
        public async Task<CreateUpdateMpesaTransactionDto> GetByCheckoutRequestIdAsync(string id)
        {
            return await _mpesaTransactionAppService.GetByCheckoutRequestIdAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateMpesaTransactionDto dto)
        {
            await _mpesaTransactionAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _mpesaTransactionAppService.DeleteAsync(id);
        }
    }
}
