using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Profile;
using Customer.Dtos.Customer;
using Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Users;
using Volo.Abp.Validation;

namespace Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;

public class AccountProfilePersonalInfoManagementGroupViewComponent : AbpViewComponent
{
    private readonly IDistributedCache<ProfileDto> _cache;
    private readonly ICurrentUser CurrentUser;

    public AccountProfilePersonalInfoManagementGroupViewComponent(
        IDistributedCache<ProfileDto> cache,
        ICurrentUser currentUser
    )
    {
        _cache = cache;
        CurrentUser = currentUser;  
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new PersonalInfoModel();
        var user = await _cache.GetAsync(string.Format(eCommerceCacheKeys.Profile, CurrentUser.Id));

        if (user == null)
            return View("~/Pages/Account/Components/ProfileManagementGroup/PersonalInfo/Default.cshtml", model);

        model = ObjectMapper.Map<ProfileDto, PersonalInfoModel>(user);
        return View("~/Pages/Account/Components/ProfileManagementGroup/PersonalInfo/Default.cshtml", model);
    }

    public class PersonalInfoModel : ExtensibleObject, IHasConcurrencyStamp
    {
        [HiddenInput]
        public string ConcurrencyStamp { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
        [Display(Name = "DisplayName:UserName")]
        public string UserName { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        [Display(Name = "DisplayName:Email")]
        public string Email { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxNameLength))]
        [Display(Name = "DisplayName:Name")]
        public string Name { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
        [Display(Name = "DisplayName:Surname")]
        public string Surname { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        [Display(Name = "DisplayName:PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}