using Abp.eCommerce.Dtos.Checkout;
using Abp.eCommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Order.Interfaces;
using PaymentTransactions.Interfaces;
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
    [Route("api/eCommerce/checkout")]
    public class CheckoutController : eCommerceController, ICheckoutAppService
    {
        #region Fields
        private readonly ICheckoutAppService _checkoutAppService;
        #endregion

        #region CTOR
        public CheckoutController(
            ICheckoutAppService checkoutAppService    
        )
        {
            _checkoutAppService = checkoutAppService;
        }
        #endregion

        [HttpPost]
        [Route("checkout")]
        public async Task<CheckoutResDto> CheckoutAsync(CheckoutDto dto)
        {
            return await _checkoutAppService.CheckoutAsync(dto);
        }
    }
}
