using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.BillingAddress;
using Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.Password;
using Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.ShippingAddress;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Volo.Abp.Account.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Abp.eCommerce.Web.Public.PageManagement
{
    public class AccountProfileManagementPageContributor : IProfileManagementPageContributor
    {
        public async Task ConfigureAsync(ProfileManagementPageCreationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<eCommerceResource>>();

            if (await IsPasswordChangeEnabled(context))
            {
                context.Groups.Add(
                    new ProfileManagementPageGroup(
                        "Volo.Abp.Account.Password",
                        l["ProfileTab:Password"],
                        typeof(AccountProfilePasswordManagementGroupViewComponent)
                    )
                );
            }

            context.Groups.Add(
                new ProfileManagementPageGroup(
                    "Volo.Abp.Account.PersonalInfo",
                    l["ProfileTab:PersonalInfo"],
                    typeof(AccountProfilePersonalInfoManagementGroupViewComponent)
                )
            );

            context.Groups.Add(
                new ProfileManagementPageGroup(
                    "Volo.Abp.Account.BillingAddress",
                    l["ProfileTab:BillingAddress"],
                    typeof(BillingAddressViewComponent)
                )
            );

            context.Groups.Add(
                new ProfileManagementPageGroup(
                    "Volo.Abp.Account.ShippingAddress",
                    l["ProfileTab:ShippingAddress"],
                    typeof(ShippingAddressViewComponent)
                )
            );
        }

        protected virtual async Task<bool> IsPasswordChangeEnabled(ProfileManagementPageCreationContext context)
        {
            var userManager = context.ServiceProvider.GetRequiredService<IdentityUserManager>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var user = await userManager.GetByIdAsync(currentUser.GetId());

            return !user.IsExternal;
        }
    }
}
