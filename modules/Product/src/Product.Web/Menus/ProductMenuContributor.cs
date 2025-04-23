using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Product.Web.Menus;

public class ProductMenuContributor : IMenuContributor
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
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductMenus.Prefix, 
                displayName: "Product", 
                "~/Product", 
                icon: "fa fa-boxes-stacked"
            )
        );

        return Task.CompletedTask;
    }
}
