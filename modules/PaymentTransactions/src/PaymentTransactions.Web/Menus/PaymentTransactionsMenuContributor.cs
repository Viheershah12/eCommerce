using Abp.eCommerce.Localization;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace PaymentTransactions.Web.Menus;

public class PaymentTransactionsMenuContributor : IMenuContributor
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
                PaymentTransactionsMenus.Prefix, 
                displayName: l["Menu:PaymentTransaction"], 
                "~/PaymentTransaction", 
                icon: "fa fa-globe"
            )
        );

        return Task.CompletedTask;
    }
}
