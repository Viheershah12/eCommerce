using PaymentTransactions.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PaymentTransactions;

public abstract class PaymentTransactionsController : AbpControllerBase
{
    protected PaymentTransactionsController()
    {
        LocalizationResource = typeof(PaymentTransactionsResource);
    }
}
