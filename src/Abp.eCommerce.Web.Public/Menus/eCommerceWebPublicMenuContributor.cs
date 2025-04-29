using System.Threading.Tasks;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Permissions;
using Abp.eCommerce.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace Abp.eCommerce.Web.Public.Menus;

public class eCommerceWebPublicMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<eCommerceResource>();

        context.Menu.TryRemoveMenuItem(DefaultMenuNames.Application.Main.Administration);

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                eCommerceWebPublicMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                eCommerceWebPublicMenus.Store,
                l["Menu:Store"],
                "~/Store",
                icon: "fa fa-home",
                order: 2
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                eCommerceWebPublicMenus.About,
                l["Menu:About"],
                "~/About",
                icon: "fa fa-home",
                order: 3
            )
        );
        
        return Task.CompletedTask;
    }
}
