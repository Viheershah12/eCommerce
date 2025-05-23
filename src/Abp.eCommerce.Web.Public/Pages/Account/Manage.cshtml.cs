using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Web.Pages.Account;
using Abp.eCommerce.Web.Public.PageManagement;
using Volo.Abp.Caching;
using Abp.eCommerce.Models;
using Volo.Abp.Account;
using System.Collections.Generic;
using Newtonsoft.Json;
using Volo.Abp.Data;
using Abp.eCommerce.Web.Public.Models.Profile;

namespace Abp.eCommerce.Web.Public.Pages.Account;

public class ManageModel : AccountPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public ProfileManagementPageCreationContext ProfileManagementPageCreationContext { get; private set; }

    protected ProfileManagementPageOptions Options { get; }

    private readonly IDistributedCache<UserAddress> _addressCache;
    private readonly IDistributedCache<ProfileDto> _profileCache;
    private IProfileAppService _profileAppService;

    public ManageModel(
        IOptions<ProfileManagementPageOptions> options,
        IDistributedCache<UserAddress> addressCache,
        IDistributedCache<ProfileDto> profileCache,
        IProfileAppService profileAppService
    )
    {
        Options = options.Value;
        _profileCache = profileCache;
        _addressCache = addressCache;
        _profileAppService = profileAppService;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        ProfileManagementPageCreationContext = new ProfileManagementPageCreationContext(ServiceProvider);

        foreach (var contributor in Options.Contributors)
        {
            await contributor.ConfigureAsync(ProfileManagementPageCreationContext);
        }

        if (ReturnUrl != null)
        {
            if (!Url.IsLocalUrl(ReturnUrl) &&
                !ReturnUrl.StartsWith(UriHelper.BuildAbsolute(Request.Scheme, Request.Host, Request.PathBase).RemovePostFix("/")) &&
                !await AppUrlProvider.IsRedirectAllowedUrlAsync(ReturnUrl))
            {
                ReturnUrl = null;
            }
        }

        // Cache Address
        var profile = await _profileAppService.GetAsync();

        if (profile != null)
        {
            var billingAddressDictionary = profile.GetProperty("BillingAddress");
            var billingAddressJson = JsonConvert.SerializeObject(billingAddressDictionary);
            var billingAddress = JsonConvert.DeserializeObject<UserAddress?>(billingAddressJson) ?? new UserAddress();

            var shippingAddressDictionary = profile.GetProperty("ShippingAddress");
            var shippingAddressJson = JsonConvert.SerializeObject(shippingAddressDictionary);
            var shippingAddress = JsonConvert.DeserializeObject<UserAddress?>(shippingAddressJson) ?? new UserAddress();

            await _profileCache.SetAsync(string.Format(eCommerceCacheKeys.Profile, CurrentUser.Id), profile);
            await _addressCache.SetAsync(string.Format(eCommerceCacheKeys.BillingAddress, CurrentUser.Id), billingAddress);
            await _addressCache.SetAsync(string.Format(eCommerceCacheKeys.ShippingAddress, CurrentUser.Id), shippingAddress);
        }

        return Page();
    }

    public virtual Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }
}
