using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Product.Localization;
using Product.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Product.Permissions;
using Abp.eCommerce.Localization;

namespace Product.Web;

[DependsOn(
    typeof(ProductApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class ProductWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(eCommerceResource), typeof(ProductWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ProductWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ProductMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ProductWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<ProductWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ProductWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            // Product 
            options.Conventions.AuthorizePage("/Product/Index", ProductPermissions.Product.Default);
            options.Conventions.AuthorizePage("/Product/Create", ProductPermissions.Product.Create);
            options.Conventions.AuthorizePage("/Product/Edit", ProductPermissions.Product.View);

            // Product Category
            options.Conventions.AuthorizePage("/ProductCategory/Index", ProductPermissions.ProductCategory.Default);
            options.Conventions.AuthorizePage("/ProductCategory/Create", ProductPermissions.ProductCategory.Create);
            options.Conventions.AuthorizePage("/ProductCategory/Edit", ProductPermissions.ProductCategory.View);

            // Product Tag
            options.Conventions.AuthorizePage("/ProductTag/Index", ProductPermissions.ProductTag.Default);
            options.Conventions.AuthorizePage("/ProductTag/Create", ProductPermissions.ProductTag.Create);
            options.Conventions.AuthorizePage("/ProductTag/Edit", ProductPermissions.ProductTag.View);
        });
    }
}
