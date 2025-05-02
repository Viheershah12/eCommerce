using Abp.eCommerce.Localization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.eCommerce.Web.Public.Models.Common;

[Authorize]
public abstract class eCommerceWebPublicPageModel : AbpPageModel
{
    protected eCommerceWebPublicPageModel()
    {
        LocalizationResourceType = typeof(eCommerceResource);
    }
}
