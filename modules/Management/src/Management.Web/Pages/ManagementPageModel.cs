using Management.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Management.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ManagementPageModel : AbpPageModel
{
    protected ManagementPageModel()
    {
        LocalizationResourceType = typeof(ManagementResource);
        ObjectMapperContext = typeof(ManagementWebModule);
    }
}
