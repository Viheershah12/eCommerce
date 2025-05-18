using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;

namespace Abp.eCommerce.HangfireJobArgs
{
    [BackgroundJobName("mpesatransactioncheck")]
    public class MpesaTransactionCheckArgs
    {
        public Guid PaymentTransactionId { get; set; }
    }
}
