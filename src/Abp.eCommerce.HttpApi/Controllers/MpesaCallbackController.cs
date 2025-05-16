using Abp.eCommerce.Dtos.MpesaCallback;
using Abp.eCommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Product;
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
    [Route("api/eCommerce/mpesacallback")]
    public class MpesaCallbackController : eCommerceController, IMpesaCallbackAppService
    {
        private readonly IMpesaCallbackAppService _mpesaCallbackAppService;

        public MpesaCallbackController(
            IMpesaCallbackAppService mpesaCallbackAppService
        )
        {
            _mpesaCallbackAppService = mpesaCallbackAppService;
        }

        [HttpPost]
        [Route("handleStkCallback")]
        public async Task HandleStkCallbackAsync(MpesaStkCallbackDto input)
        {
            await _mpesaCallbackAppService.HandleStkCallbackAsync(input);
        }
    }
}
