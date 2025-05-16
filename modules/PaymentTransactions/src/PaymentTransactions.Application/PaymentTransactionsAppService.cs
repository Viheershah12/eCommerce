using PaymentTransactions.Localization;
using Volo.Abp.Application.Services;

namespace PaymentTransactions;

public abstract class PaymentTransactionsAppService : ApplicationService
{
    protected PaymentTransactionsAppService()
    {
        LocalizationResource = typeof(PaymentTransactionsResource);
        ObjectMapperContext = typeof(PaymentTransactionsApplicationModule);
    }
}
