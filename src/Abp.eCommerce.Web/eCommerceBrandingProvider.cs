using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Abp.eCommerce.Localization;

namespace Abp.eCommerce.Web;

[Dependency(ReplaceServices = true)]
public class eCommerceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<eCommerceResource> _localizer;

    public eCommerceBrandingProvider(IStringLocalizer<eCommerceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
