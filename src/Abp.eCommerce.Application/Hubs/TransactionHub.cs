using Abp.eCommerce.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Abp.eCommerce.Hubs
{
    [Authorize]
    public class TransactionHub : AbpHub
    {
        #region Fields
        private readonly IMpesaAppService _mpesaAppService;
        private readonly ILogger<TransactionHub> _logger;
        #endregion

        #region CTOR
        public TransactionHub(
            IMpesaAppService mpesaAppService,
            ILogger<TransactionHub> logger
        )
        {
            _mpesaAppService = mpesaAppService;
            _logger = logger;
        }
        #endregion

        public override Task OnConnectedAsync()
        {
            var user = CurrentUser;

            _logger.LogInformation("Client connected: {ConnectionId}, User: {User}",
                Context.ConnectionId,
                Context.UserIdentifier);

            return base.OnConnectedAsync();
        }

        public async Task CheckMpesaTransactionStatus(Guid transactionId, Guid targetUserId)
        {
            await _mpesaAppService.CheckTransactionAsync(transactionId);
        }
    }
}
