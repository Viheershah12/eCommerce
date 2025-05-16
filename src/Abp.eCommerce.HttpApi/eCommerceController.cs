using Abp.eCommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.eCommerce;

/* Inherit your controllers from this class.
 */
public abstract class eCommerceController : AbpControllerBase
{
    protected eCommerceController()
    {
        LocalizationResource = typeof(eCommerceResource);
    }
}
