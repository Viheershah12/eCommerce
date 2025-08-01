using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Abp.eCommerce.MongoDB;
using Abp.eCommerce.Localization;
using Abp.eCommerce.MultiTenancy;
using Abp.eCommerce.Permissions;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Studio;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Identity.Web;
using Volo.Abp.FeatureManagement;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp.TenantManagement.Web;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Identity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.OpenIddict;
using Volo.Abp.Security.Claims;
using Volo.Abp.Studio.Client.AspNetCore;
using Volo.Abp.Ui.LayoutHooks;
using Abp.eCommerce.Web.Common;
using Abp.eCommerce.Web.Public.Menus;
using Microsoft.AspNetCore.Identity;
using Abp.eCommerce.Web.Public.PasswordlessAuthentication;
using Volo.CmsKit.Web;
using Abp.eCommerce.Web.Public.Themes.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Abp.eCommerce.Web.Public.Toolbars;
using Abp.eCommerce.Web.Public.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Microsoft.AspNetCore.Http;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.RabbitMQ;
using Volo.Abp.AspNetCore.SignalR;
using Abp.eCommerce.Web.Public.PageManagement;
using Volo.Abp.Account.Localization;
using Abp.eCommerce.Web.Public.Pages.Account;

namespace Abp.eCommerce.Web.Public
{
    [DependsOn(
        typeof(eCommerceHttpApiModule),
        typeof(eCommerceApplicationModule),
        typeof(eCommerceMongoDbModule),
        typeof(AbpAutofacModule),
        typeof(AbpStudioClientAspNetCoreModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpAccountWebOpenIddictModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpFeatureManagementWebModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(eCommerceWebCommonModule),
        typeof(CmsKitWebModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpAspNetCoreSignalRModule)
    )]
    public class eCommerceWebPublicModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(eCommerceResource),
                    typeof(eCommerceDomainModule).Assembly,
                    typeof(eCommerceDomainSharedModule).Assembly,
                    typeof(eCommerceApplicationModule).Assembly,
                    typeof(eCommerceApplicationContractsModule).Assembly,
                    typeof(eCommerceWebPublicModule).Assembly
                );
            });

            PreConfigure<OpenIddictBuilder>(builder =>
            {
                builder.AddValidation(options =>
                {
                    options.AddAudiences("eCommerce");
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
                {
                    options.AddDevelopmentEncryptionAndSigningCertificate = false;
                });

                PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
                {
                    serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", configuration["AuthServer:CertificatePassPhrase"]!);
                    serverBuilder.SetIssuer(new Uri(configuration["AuthServer:Authority"]!));
                });
            }

            CmsBlogsWebConsts.BlogsRoutePrefix = "Blog";
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpRabbitMqOptions>(options =>
            {
                options.Connections.Default.UserName = "guest";
                options.Connections.Default.Password = "guest";
                options.Connections.Default.HostName = "127.0.0.1";
                options.Connections.Default.Port = 5672;
            });

            if (!configuration.GetValue<bool>("App:DisablePII"))
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
            }

            if (!configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata"))
            {
                Configure<OpenIddictServerAspNetCoreOptions>(options =>
                {
                    options.DisableTransportSecurityRequirement = true;
                });

                Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
                });
            }

            Configure<AbpThemingOptions>(options =>
            {
                options.Themes.Add<BasicTheme>();

                if (options.DefaultThemeName == null)
                {
                    options.DefaultThemeName = BasicTheme.Name;
                }
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new eCommerceBasicThemeToolbarContributor());
            });

            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(eCommerceBasicThemeBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(StandardBundles.Styles.Global)
                            .AddContributors(typeof(eCommerceBasicThemeGlobalStyleContributor))
                            .AddFiles("/global-styles.css");
                    });

                options
                    .ScriptBundles
                    .Add(eCommerceBasicThemeBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(StandardBundles.Scripts.Global)
                            .AddContributors(typeof(eCommerceBasicThemeGlobalScriptContributor))
                            .AddFiles("/global-scripts.js");
                    });
            });

            ConfigureUrls(configuration);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
            ConfigureCustom();
            ConfigureProfileManagement();

            context.Services
                .GetObject<IdentityBuilder>()
                .AddDefaultTokenProviders()
                .AddPasswordlessLoginProvider();
        }

        private void ConfigureProfileManagement()
        {
            Configure<PermissionManagementOptions>(options =>
            {
                options.IsDynamicPermissionStoreEnabled = true;
            });

            Configure<AbpBundlingOptions>(options =>
            {
                options.ScriptBundles
                    .Configure(typeof(ManageModel).FullName,
                        configuration =>
                        {
                            configuration.AddFiles("/client-proxies/account-proxy.js");
                            configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/Password/Default.js");
                            configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/PersonalInfo/Default.js");
                            configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/BillingAddress/Default.js");
                            configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/ShippingAddress/Default.js");
                        });
            });
        }

        private void ConfigureCustom()
        {
            Configure<AbpLayoutHookOptions>(options =>
            {
                options.Add(
                    LayoutHooks.Body.Last,
                        typeof(NotifyViewComponent)
                );
            });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
            {
                options.IsDynamicClaimsEnabled = true;
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<eCommerceWebPublicModule>();
            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<eCommerceWebPublicModule>();

                if (hostingEnvironment.IsDevelopment())
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<eCommerceDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Abp.eCommerce.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<eCommerceDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Abp.eCommerce.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<eCommerceApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Abp.eCommerce.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<eCommerceApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Abp.eCommerce.Application", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<eCommerceHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Abp.eCommerce.HttpApi", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<eCommerceWebPublicModule>(hostingEnvironment.ContentRootPath);
                }
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new eCommerceWebPublicMenuContributor());
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new eCommerceWebPublicToolbarContributor());
            });

            Configure<ProfileManagementPageOptions>(options =>
            {
                options.Contributors.Add(new AccountProfileManagementPageContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(eCommerceApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseCorrelationId();
            app.MapAbpStaticAssets();
            app.UseAbpStudioLink();
            app.UseRouting();
            app.UseAbpSecurityHeaders();
            app.UseAuthentication();
            app.UseAbpOpenIddictValidation();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseDynamicClaims();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "eCommerce API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
