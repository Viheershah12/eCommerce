using Abp.eCommerce.Etos.MpesaTransaction;
using Abp.eCommerce.Web.Public.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Abp.eCommerce.Web.Public.EventHandler
{
    public class MpesaTransactionStatusEventHandler : IDistributedEventHandler<MpesaTransactionStatusEto>, ITransientDependency
    {
        private readonly IHubContext<TransactionHub> _hubContext;

        public MpesaTransactionStatusEventHandler(IHubContext<TransactionHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleEventAsync(MpesaTransactionStatusEto eto)
        {
            var status = (int)eto.Status;

            await _hubContext.Clients
                .User(eto.CustomerId.ToString())
                .SendAsync("ReceiveTransactionStatus", status.ToString());
        }
    }
}
