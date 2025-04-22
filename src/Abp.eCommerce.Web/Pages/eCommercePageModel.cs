using Abp.eCommerce.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.eCommerce.Web.Pages;

public abstract class eCommercePageModel : AbpPageModel
{
    protected eCommercePageModel()
    {
        LocalizationResourceType = typeof(eCommerceResource);
    }
}
