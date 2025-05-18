using Abp.eCommerce.Enums;
using Abp.eCommerce.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Abp.eCommerce.Web.Public.Hubs
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

        public async Task CheckMpesaTransactionStatus(Guid transactionId)
        {
            await _mpesaAppService.CheckTransactionAsync(transactionId);
        }
    }
}
