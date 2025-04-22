using Abp.eCommerce.Localization;
using Volo.Abp.Application.Services;

namespace Abp.eCommerce;

/* Inherit your application services from this class.
 */
public abstract class eCommerceAppService : ApplicationService
{
    protected eCommerceAppService()
    {
        LocalizationResource = typeof(eCommerceResource);
    }
}
