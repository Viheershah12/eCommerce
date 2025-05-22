using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Abp.eCommerce;

public static class eCommerceDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            /* You can add extension properties to DTOs
             * defined in the depended modules.
             *
             * Example:
             *
             * ObjectExtensionManager.Instance
             *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
             *
             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Object-Extensions
             */

            ObjectExtensionManager.Instance
                .AddOrUpdate<IdentityUser>(options =>
                {
                    options.AddOrUpdateProperty<string?>("HomePhoneNumber");
                    options.AddOrUpdateProperty(typeof(Gender?), "Gender");
                    options.AddOrUpdateProperty<DateTime?>("DateOfBirth");
                    options.AddOrUpdateProperty<UserAddress?>("BillingAddress");
                    options.AddOrUpdateProperty<UserAddress?>("ShippingAddress");
                    options.AddOrUpdateProperty(typeof(IdentificationType?), "IdentificationType");
                    options.AddOrUpdateProperty<string?>("IdentificationNo");
                });
        });
    }
}
