using Abp.eCommerce.Localization;
using Product.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
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
        var l = context.GetLocalizer<eCommerceResource>();

        //Add main menu items.
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductMenus.Prefix, 
                displayName: l["Menu:Catalog"], 
                "~/Product", 
                icon: "fa fa-boxes-stacked"
            )
            .RequirePermissions(false, [ProductPermissions.Product.Default, ProductPermissions.ProductCategory.Default, ProductPermissions.ProductTag.Default])
            .AddItem(
                new ApplicationMenuItem(
                   ProductMenus.Product,
                    displayName: l["Menu:Product"],
                    "~/Product"
                )
                .RequirePermissions([ProductPermissions.Product.Default])
            )
            .AddItem(
                new ApplicationMenuItem(
                   ProductMenus.ProductCategory,
                    displayName: l["Menu:ProductCategory"],
                    "~/ProductCategory"
                )
                .RequirePermissions([ProductPermissions.ProductCategory.Default])
            )
            .AddItem(
                new ApplicationMenuItem(
                    ProductMenus.ProductTag,
                    displayName: l["Menu:ProductTag"],
                    "~/ProductTag"
                )
                .RequirePermissions([ProductPermissions.ProductTag.Default])
            )
        );

        return Task.CompletedTask;
    }
}
