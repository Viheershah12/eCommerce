using Abp.eCommerce.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.eCommerce.Web.Public.Models.Common;

public abstract class eCommerceWebPublicPageModel : AbpPageModel
{
    protected eCommerceWebPublicPageModel()
    {
        LocalizationResourceType = typeof(eCommerceResource);
    }
}
