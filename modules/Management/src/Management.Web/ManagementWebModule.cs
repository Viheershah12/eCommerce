using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Management.Localization;
using Management.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Management.Permissions;
using Abp.eCommerce.Localization;

namespace Management.Web;

[DependsOn(
    typeof(ManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class ManagementWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(eCommerceResource), typeof(ManagementWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ManagementWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ManagementMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ManagementWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<ManagementWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ManagementWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            // Content Management
            options.Conventions.AuthorizePage("/ContentManagement/Index", ManagementPermissions.ContentManagement.Default);
            options.Conventions.AuthorizePage("/ContentManagement/Create", ManagementPermissions.ContentManagement.Create);
            options.Conventions.AuthorizePage("/ContentManagement/Edit", ManagementPermissions.ContentManagement.View);
        });
    }
}
