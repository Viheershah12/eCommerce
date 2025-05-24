using System.Threading.Tasks;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Permissions;
using Abp.eCommerce.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Management.Web.Menus;
using Volo.CmsKit.Admin.Web.Menus;

namespace Abp.eCommerce.Web.Menus;

public class eCommerceMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<eCommerceResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                eCommerceMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 2;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
    
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
        
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);

        // CMS
        var cms = administration.Items.Find(x => x.Name == CmsKitAdminMenus.GroupName);
        var management = context.Menu.FindMenuItem(ManagementMenus.Prefix);

        if (cms != null && management != null)
        {
            cms.Icon = string.Empty;
            cms.Order = 2;
            cms.Items.RemoveAll(x => 
                x.Name == CmsKitAdminMenus.Pages.PagesMenu ||
                x.Name == CmsKitAdminMenus.Menus.MenusMenu);

            management.Items.Add(cms);
        }

        administration.TryRemoveMenuItem(CmsKitAdminMenus.GroupName);

        return Task.CompletedTask;
    }
}
