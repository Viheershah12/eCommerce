using Abp.eCommerce.Enums;
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
            var type = "";
            var redirectUrl = "";

            switch (eto.Status)
            {
                case PaymentTransactionStatus.Pending:
                case PaymentTransactionStatus.PendingConfirmed:
                    type = "warning";
                    redirectUrl = $"/Orders/Detail?id={eto.OrderId}";

                    break;

                case PaymentTransactionStatus.Completed:
                    type = "success";
                    redirectUrl = $"/Orders/Detail?id={eto.OrderId}";

                    break;

                case PaymentTransactionStatus.Failed:
                case PaymentTransactionStatus.Cancelled:
                    type = "error";
                    redirectUrl = $"/Orders/Detail?id={eto.OrderId}";

                    break;
            }

            var messagePayload = new
            {
                message = eto.Message,
                type,
                redirectUrl,
            };

            await _hubContext.Clients
                .User(eto.CustomerId.ToString())
                .SendAsync("ReceiveNotification", messagePayload);
        }
    }
}
