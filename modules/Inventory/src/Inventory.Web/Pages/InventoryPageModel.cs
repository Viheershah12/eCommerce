using Inventory.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Inventory.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class InventoryPageModel : AbpPageModel
{
    protected InventoryPageModel()
    {
        LocalizationResourceType = typeof(InventoryResource);
        ObjectMapperContext = typeof(InventoryWebModule);
    }
}
