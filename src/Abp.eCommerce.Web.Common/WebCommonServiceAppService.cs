using Abp.eCommerce.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Localization;

namespace Abp.eCommerce.Web.Common
{
    public class WebCommonServiceAppService : ApplicationService
    {
        protected WebCommonServiceAppService()
        {
            LocalizationResource = typeof(eCommerceResource);
        }
    }
}
