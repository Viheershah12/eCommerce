
using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Order;
using Abp.eCommerce.Web.Public.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Users;

namespace Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.BillingAddress
{
    public class BillingAddressViewComponent : AbpViewComponent
    {
        private readonly IDistributedCache<UserAddress> _cache;
        private readonly ICurrentUser CurrentUser;

        public BillingAddressViewComponent(
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
            var billingAddress = await _cache.GetAsync(string.Format(eCommerceCacheKeys.BillingAddress, CurrentUser.Id));

            if (billingAddress == null)
                return View("~/Pages/Account/Components/ProfileManagementGroup/BillingAddress/Default.cshtml", model);

            model = ObjectMapper.Map<UserAddress?, UserAddressViewModel?>(billingAddress);
            return View("~/Pages/Account/Components/ProfileManagementGroup/BillingAddress/Default.cshtml", model);
        }
    }
}
