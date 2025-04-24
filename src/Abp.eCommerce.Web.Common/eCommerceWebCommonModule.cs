using Abp.eCommerce.Localization;
using Localization.Resources.AbpUi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.eCommerce.Web.Common
{
    public class eCommerceWebCommonModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<eCommerceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<eCommerceWebCommonModule>("Abp.eCommerce.Web.Common");
            });

            // Dink to PDF
            //context.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }
    }
}
