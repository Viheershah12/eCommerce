using Order.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Order.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class OrderPageModel : AbpPageModel
{
    protected OrderPageModel()
    {
        LocalizationResourceType = typeof(OrderResource);
        ObjectMapperContext = typeof(OrderWebModule);
    }
}
