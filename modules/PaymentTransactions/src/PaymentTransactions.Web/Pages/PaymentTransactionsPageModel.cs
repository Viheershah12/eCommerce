using PaymentTransactions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace PaymentTransactions.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PaymentTransactionsPageModel : AbpPageModel
{
    protected PaymentTransactionsPageModel()
    {
        LocalizationResourceType = typeof(PaymentTransactionsResource);
        ObjectMapperContext = typeof(PaymentTransactionsWebModule);
    }
}
