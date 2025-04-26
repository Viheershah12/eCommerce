using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Customer.Localization;
using Customer.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Customer.Permissions;

namespace Customer.Web;

[DependsOn(
    typeof(CustomerApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class CustomerWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(CustomerResource), typeof(CustomerWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CustomerWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CustomerMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CustomerWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<CustomerWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CustomerWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            // Customer List
            options.Conventions.AuthorizePage("/CustomerList/Index", CustomerPermissions.CustomerList.List);
            options.Conventions.AuthorizePage("/CustomerList/Create", CustomerPermissions.CustomerList.Create);
            options.Conventions.AuthorizePage("/CustomerList/Edit", CustomerPermissions.CustomerList.Edit);

            // Customer Group
            options.Conventions.AuthorizePage("/CustomerGroup/Index", CustomerPermissions.CustomerGroup.List);
            options.Conventions.AuthorizePage("/CustomerGroup/Create", CustomerPermissions.CustomerGroup.Create);
            options.Conventions.AuthorizePage("/CustomerGroup/Edit", CustomerPermissions.CustomerGroup.Edit);
        });
    }
}
