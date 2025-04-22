using Microsoft.Extensions.Localization;
using Volo.Abp.Ui.Branding;
using Abp.eCommerce.Localization;
using Volo.Abp.DependencyInjection;

namespace Abp.eCommerce;

[Dependency(ReplaceServices = true)]
public class eCommerceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<eCommerceResource> _localizer;

    public eCommerceBrandingProvider(IStringLocalizer<eCommerceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AuthAppName"];
}
