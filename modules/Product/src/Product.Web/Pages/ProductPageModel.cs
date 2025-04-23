using Product.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Product.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ProductPageModel : AbpPageModel
{
    protected ProductPageModel()
    {
        LocalizationResourceType = typeof(ProductResource);
        ObjectMapperContext = typeof(ProductWebModule);
    }
}
