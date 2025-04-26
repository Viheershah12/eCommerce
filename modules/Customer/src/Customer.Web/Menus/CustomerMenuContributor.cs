using Abp.eCommerce.Localization;
using Customer.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Customer.Web.Menus;

public class CustomerMenuContributor : IMenuContributor
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
                CustomerMenus.Prefix, 
                displayName: l["Menu:Customer"], 
                "~/Customer", 
                icon: "fa fa-user",
                1001
            )
            .RequirePermissions(false, [CustomerPermissions.CustomerList.Default, CustomerPermissions.CustomerGroup.Default])
            .AddItem(
                new ApplicationMenuItem(
                    CustomerMenus.CustomerList,
                    displayName: l["Menu:CustomerList"],
                    "~/CustomerList"
                )    
                .RequirePermissions(CustomerPermissions.CustomerList.Default)
            )
            .AddItem(
                new ApplicationMenuItem(
                    CustomerMenus.CustomerGroup,
                    displayName: l["Menu:CustomerGroup"],
                    "~/CustomerGroup"
                )
                .RequirePermissions(CustomerPermissions.CustomerGroup.Default)
            )
        );

        return Task.CompletedTask;
    }
}
