using Abp.eCommerce.Localization;
using Inventory.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Inventory.Web.Models.Common;

/* Inherit your PageModel classes from this class.
 */
public abstract class InventoryPageModel : AbpPageModel
{
    protected InventoryPageModel()
    {
        LocalizationResourceType = typeof(eCommerceResource);
        ObjectMapperContext = typeof(InventoryWebModule);
    }
}
