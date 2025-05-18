using Abp.eCommerce.Etos.MpesaTransaction;
using Abp.eCommerce.Interfaces;
using Abp.eCommerce.Web.Public.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Abp.eCommerce.Web.Public.EventHandler
{
    public class CheckMpesaTransactionEventHandler : IDistributedEventHandler<CheckMpesaTransactionEto>, ITransientDependency
    {
        private readonly IMpesaAppService _mpesaAppService;

        public CheckMpesaTransactionEventHandler(IMpesaAppService mpesaAppService)
        {
            _mpesaAppService = mpesaAppService;
        }

        public async Task HandleEventAsync(CheckMpesaTransactionEto eto)
        {
            await _mpesaAppService.CheckTransactionAsync(eto.PaymentTransactionId);
        }
    }
}
