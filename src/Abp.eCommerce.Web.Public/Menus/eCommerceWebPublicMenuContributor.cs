using System.Threading.Tasks;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Permissions;
using Abp.eCommerce.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.CmsKit.Admin.Blogs;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Users;

namespace Abp.eCommerce.Web.Public.Menus;

public class eCommerceWebPublicMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

        if (currentUser.IsAuthenticated)
        {

            var l = context.GetLocalizer<eCommerceResource>();
            var blogAppService = context.ServiceProvider.GetRequiredService<IBlogAdminAppService>();
            var list = await blogAppService.GetListAsync(new BlogGetListInput { MaxResultCount = 1000 });

            context.Menu.TryRemoveMenuItem(DefaultMenuNames.Application.Main.Administration);

            //Home
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    eCommerceWebPublicMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fa fa-home",
                    order: 1
                )
            );

            context.Menu.AddItem(
                new ApplicationMenuItem(
                    eCommerceWebPublicMenus.Store,
                    l["Menu:Store"],
                    "~/Store",
                    icon: "fa fa-home",
                    order: 2
                )
            );

            // Blog
            var blog = new ApplicationMenuItem(
                    eCommerceWebPublicMenus.Blog,
                    l["Menu:Blog"],
                    "~/Blog",
                    icon: "fa fa-blog",
                    order: 3
                );

            foreach (var item in list.Items)
            {
                blog.AddItem(
                    new ApplicationMenuItem(
                        item.Name,
                        item.Name,
                        $"/Blog/{item.Slug}"
                    )
                );
            }

            context.Menu.AddItem(blog);

            context.Menu.AddItem(
                new ApplicationMenuItem(
                    eCommerceWebPublicMenus.About,
                    l["Menu:About"],
                    "~/About",
                    icon: "fa fa-home",
                    order: 4
                )
            );
        }
    }
}
