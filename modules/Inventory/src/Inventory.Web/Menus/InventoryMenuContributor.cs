using Abp.eCommerce.Localization;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Inventory.Web.Menus;

public class InventoryMenuContributor : IMenuContributor
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
                InventoryMenus.Prefix, 
                displayName: l["Menu:Inventory"], 
                "~/Inventory", 
                icon: "fa fa-warehouse"
            )
            .AddItem(
                new ApplicationMenuItem(
                    InventoryMenus.StockBalance,
                    displayName: l["Menu:StockBalance"],
                    "~/StockBalance",
                    icon: "fa fa-scale-balanced"
                )
            )
            .AddItem(
                new ApplicationMenuItem(
                    InventoryMenus.StockMovement,
                    displayName: l["Menu:StockMovement"],
                    "~/StockMovement",
                    icon: "fa fa-truck"
                )
            )
        );

        return Task.CompletedTask;
    }
}
