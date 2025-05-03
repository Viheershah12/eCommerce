using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Order.Web.Menus;

public class OrderMenuContributor : IMenuContributor
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
        context.Menu.AddItem(new ApplicationMenuItem(OrderMenus.Prefix, displayName: "Order", "~/Order", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
