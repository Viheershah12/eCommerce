using Abp.eCommerce.Dtos.Mpesa;
using Abp.eCommerce.HangfireJobArgs;
using Abp.eCommerce.Interfaces;
using Hangfire;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Abp.eCommerce.HangfireJob
{
    [Queue("mpesatransaction")]
    public class MpesaTransactionCheckJob : AsyncBackgroundJob<MpesaTransactionCheckArgs>, ITransientDependency
    {
        private readonly IMpesaAppService _mpesaAppService;

        public MpesaTransactionCheckJob(IMpesaAppService mpesaAppService)
        {
            _mpesaAppService = mpesaAppService;
        }

        public override async Task ExecuteAsync(MpesaTransactionCheckArgs args)
        {
            await _mpesaAppService.CheckTransactionAsync(args.PaymentTransactionId);
        }
    }
}
