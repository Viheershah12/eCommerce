using Abp.eCommerce.Localization;
using Management.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Management.Web.Menus;

public class ManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<eCommerceResource>();

        //Add main menu items.
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ManagementMenus.Prefix, 
                displayName: l["Menu:Management"], 
                "~/Management", 
                icon: "fa fa-gear",
                1000
            )
            .RequirePermissions(false, [ManagementPermissions.ContentManagement.Default])
            .AddItem(
                new ApplicationMenuItem(
                    ManagementMenus.ContentManagement,
                    displayName: l["Menu:ContentManagement"],
                    "~/ContentManagement", 
                    order: 1
                )
                .RequirePermissions(ManagementPermissions.ContentManagement.Default)
            )
        );

        return Task.CompletedTask;
    }
}
