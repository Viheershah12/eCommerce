using Customer.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Customer.Web.Models.Common;

/* Inherit your PageModel classes from this class.
 */
public abstract class CustomerPageModel : AbpPageModel
{
    protected CustomerPageModel()
    {
        LocalizationResourceType = typeof(CustomerResource);
        ObjectMapperContext = typeof(CustomerWebModule);
    }
}
