using Abp.eCommerce.Localization;
using System.Threading.Tasks;
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
                icon: "fa fa-gear"
            )
            .AddItem(
                new ApplicationMenuItem(
                    ManagementMenus.ContentManagement,
                    displayName: l["Menu:ContentManagement"],
                    "~/ContentManagement"
                )
            )
        );

        return Task.CompletedTask;
    }
}
