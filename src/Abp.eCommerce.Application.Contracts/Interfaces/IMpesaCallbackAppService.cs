using Abp.eCommerce.Dtos.MpesaCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.eCommerce.Interfaces
{
    public interface IMpesaCallbackAppService : IApplicationService
    {
        Task HandleStkCallbackAsync(MpesaStkCallbackDto input);
    }
}
