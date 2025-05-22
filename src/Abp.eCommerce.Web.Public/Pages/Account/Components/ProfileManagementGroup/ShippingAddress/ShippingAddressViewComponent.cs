using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Order;
using Abp.eCommerce.Web.Public.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Data;
using static Order.Models.Order;

namespace Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.ShippingAddress
{
    public class ShippingAddressViewComponent : AbpViewComponent
    {
        private readonly IProfileAppService ProfileAppService;

        public ShippingAddressViewComponent(IProfileAppService profileAppService) 
        {
            ProfileAppService = profileAppService;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await ProfileAppService.GetAsync();
            var address = user.GetProperty<UserAddress?>("ShippingAddress");
            var model = ObjectMapper.Map<UserAddress?, UserAddressViewModel?>(address);

            return View("~/Pages/Account/Components/ProfileManagementGroup/ShippingAddress/Default.cshtml", model);
        }
    }
}
