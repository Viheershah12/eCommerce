using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Abp.eCommerce.Localization;

namespace Abp.eCommerce.Web.Public;

[Dependency(ReplaceServices = true)]
public class eCommerceWebPublicBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<eCommerceResource> _localizer;

    public eCommerceWebPublicBrandingProvider(IStringLocalizer<eCommerceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["PublicAppName"];
}
