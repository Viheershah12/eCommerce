using Abp.eCommerce.Dtos.Mpesa;
using Abp.eCommerce.Enums;
using Abp.eCommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.eCommerce.Controllers
{
    [Area(eCommerceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = eCommerceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/eCommerce/mpesa")]
    public class MpesaController : eCommerceController, IMpesaAppService
    {
        private readonly IMpesaAppService _mpesaAppService;

        public MpesaController(
            IMpesaAppService mpesaAppService                
        )
        {
            _mpesaAppService = mpesaAppService;
        }

        [HttpGet]
        [Route("getAccessToken")]
        public async Task<string> GetAccessTokenAsync()
        {
            return await _mpesaAppService.GetAccessTokenAsync();    
        }

        [HttpGet]
        [Route("initiateSTKPush")]
        public async Task<string> InitiateSTKPushAsync(MpesaStkPushRequestDto input)
        {
            return await _mpesaAppService.InitiateSTKPushAsync(input);
        }

        [HttpGet]
        [Route("checkTransaction")]
        public async Task CheckTransactionAsync(Guid transactionId)
        {
            await _mpesaAppService.CheckTransactionAsync(transactionId);
        }

        [HttpGet]
        [Route("checkTransactionStatus")]
        public async Task CheckTransactionStatusAsync()
        {
            await _mpesaAppService.CheckTransactionStatusAsync();   
        }
    }
}
