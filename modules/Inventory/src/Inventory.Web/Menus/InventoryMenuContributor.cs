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
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(InventoryMenus.Prefix, displayName: "Inventory", "~/Inventory", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
