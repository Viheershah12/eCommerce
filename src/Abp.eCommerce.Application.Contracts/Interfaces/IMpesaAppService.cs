using Abp.eCommerce.Dtos.Mpesa;
using Abp.eCommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.eCommerce.Interfaces
{
    public interface IMpesaAppService : IApplicationService
    {
        Task<string> GetAccessTokenAsync();

        Task<string> InitiateSTKPushAsync(MpesaStkPushRequestDto input);

        Task CheckTransactionAsync(Guid transactionId);

        Task CheckTransactionStatusAsync();
    }
}
