using Hangfire;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Abp.eCommerce.Interfaces;

namespace Abp.eCommerce.HttpApi.Host.HangfireServices
{
    public class MpesaBackgroundWorker : HangfireBackgroundWorkerBase
    {
        private readonly IMpesaAppService _mpesaAppService;

        public MpesaBackgroundWorker(IMpesaAppService mpessaAppService)
        {
            _mpesaAppService = mpessaAppService;

            RecurringJobId = nameof(MpesaBackgroundWorker);
            CronExpression = Cron.Minutely();
        }

        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            await _mpesaAppService.CheckTransactionStatusAsync();
            Logger.LogInformation("Executed Mpesa HangfireBackgroundWorker..!");
            await Task.CompletedTask;
        }
    }
}
