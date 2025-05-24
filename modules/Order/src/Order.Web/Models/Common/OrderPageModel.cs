using Abp.eCommerce.Localization;
using Order.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Order.Web.Models.Common;

/* Inherit your PageModel classes from this class.
 */
public abstract class OrderPageModel : AbpPageModel
{
    protected OrderPageModel()
    {
        LocalizationResourceType = typeof(eCommerceResource);
        ObjectMapperContext = typeof(OrderWebModule);
    }
}
