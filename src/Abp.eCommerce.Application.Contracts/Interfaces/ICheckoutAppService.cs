using Abp.eCommerce.Dtos.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.eCommerce.Interfaces
{
    public interface ICheckoutAppService : IApplicationService
    {
        Task<Guid> CheckoutAsync(CheckoutDto dto);
    }
}
