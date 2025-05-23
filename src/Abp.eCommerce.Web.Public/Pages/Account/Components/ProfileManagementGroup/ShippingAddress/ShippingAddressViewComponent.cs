using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Order;
using Abp.eCommerce.Web.Public.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Users;
using static Order.Models.Order;

namespace Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.ShippingAddress
{
    public class ShippingAddressViewComponent : AbpViewComponent
    {
        private readonly IDistributedCache<UserAddress> _cache;
        private readonly ICurrentUser CurrentUser;

        public ShippingAddressViewComponent(
            IDistributedCache<UserAddress> cache, 
            ICurrentUser currentUser
        ) 
        {
            _cache = cache;
            CurrentUser = currentUser;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new UserAddressViewModel();
            var shippingAddress = await _cache.GetAsync(string.Format(eCommerceCacheKeys.ShippingAddress, CurrentUser.Id));

            if (shippingAddress == null)
                return View("~/Pages/Account/Components/ProfileManagementGroup/ShippingAddress/Default.cshtml", model);

            model = ObjectMapper.Map<UserAddress?, UserAddressViewModel?>(shippingAddress);
            return View("~/Pages/Account/Components/ProfileManagementGroup/ShippingAddress/Default.cshtml", model);
        }
    }
}
