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
        //Add main menu items.
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ManagementMenus.Prefix, 
                displayName: "Management", 
                "~/Management", 
                icon: "fa fa-gear"
            )
        );

        return Task.CompletedTask;
    }
}
