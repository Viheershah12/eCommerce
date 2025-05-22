using Abp.eCommerce.Enums;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Abp.eCommerce;

public static class eCommerceModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
            ConfigureIdentityProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {
        /* You can change max lengths for properties of the
         * entities defined in the modules used by your application.
         *
         * Example: Change user and role name max lengths

           AbpUserConsts.MaxNameLength = 99;
           IdentityRoleConsts.MaxNameLength = 99;

         * Notice: It is not suggested to change property lengths
         * unless you really need it. Go with the standard values wherever possible.
         *
         * If you are using EF Core, you will need to run the add-migration command after your changes.
         */
    }

    private static void ConfigureExtraProperties()
    {
        /* You can configure extra properties for the
         * entities defined in the modules used by your application.
         *
         * This class can be used to define these extra properties
         * with a high level, easy to use API.
         *
         * Example: Add a new property to the user entity of the identity module

           ObjectExtensionManager.Instance.Modules()
              .ConfigureIdentity(identity =>
              {
                  identity.ConfigureUser(user =>
                  {
                      user.AddOrUpdateProperty<string>( //property type: string
                          "SocialSecurityNumber", //property name
                          property =>
                          {
                              //validation rules
                              property.Attributes.Add(new RequiredAttribute());
                              property.Attributes.Add(new StringLengthAttribute(64) {MinimumLength = 4});

                              //...other configurations for this property
                          }
                      );
                  });
              });

         * See the documentation for more:
         * https://docs.abp.io/en/abp/latest/Module-Entity-Extensions
         */
    }

    private static void ConfigureIdentityProperties()
    {
        ObjectExtensionManager.Instance.Modules()
           .ConfigureIdentity(identity =>
           {
               identity.ConfigureUser(user =>
               {
                   user.AddOrUpdateProperty<string?>("HomePhoneNumber", property =>
                   {
                       property.Configuration.Add(IdentityModuleExtensionConsts.ConfigurationNames.AllowUserToEdit, true);
                       property.IsAvailableToClients = true;
                       property.UI.Order = 1;
                       property.DisplayName = LocalizableString.Create<eCommerceResource>("DisplayName:HomePhoneNumber");
                   });

                   user.AddOrUpdateProperty(typeof(Gender), "Gender", property =>
                   {
                       property.Configuration.Add(IdentityModuleExtensionConsts.ConfigurationNames.AllowUserToEdit, true);
                       property.IsAvailableToClients = true;
                       property.UI.Order = 3;
                       property.DisplayName = LocalizableString.Create<eCommerceResource>("DisplayName:Gender");
                       property.DefaultValue = 0;
                   });

                   user.AddOrUpdateProperty(typeof(DateTime), "DateOfBirth", property =>
                   {
                       property.Configuration.Add(IdentityModuleExtensionConsts.ConfigurationNames.AllowUserToEdit, true);
                       property.IsAvailableToClients = true;
                       property.UI.Order = 2;
                       property.DisplayName = LocalizableString.Create<eCommerceResource>("DisplayName:DateOfBirth");
                   });

                   user.AddOrUpdateProperty(typeof(IdentificationType), "IdentificationType", property =>
                   {
                       property.Configuration.Add(IdentityModuleExtensionConsts.ConfigurationNames.AllowUserToEdit, true);
                       property.IsAvailableToClients = true;
                       property.UI.Order = 4;
                       property.DisplayName = LocalizableString.Create<eCommerceResource>("DisplayName:IdentificationType");
                       property.DefaultValue = 0;
                   });

                   user.AddOrUpdateProperty<string?>("IdentificationNo", property =>
                   {
                       property.Configuration.Add(IdentityModuleExtensionConsts.ConfigurationNames.AllowUserToEdit, true);
                       property.IsAvailableToClients = true;
                       property.UI.Order = 5;
                       property.DisplayName = LocalizableString.Create<eCommerceResource>("DisplayName:IdentificationNo");
                   });

                   user.AddOrUpdateProperty<UserAddress?>("BillingAddress");
                   user.AddOrUpdateProperty<UserAddress?>("ShippingAddress");
               });
           });
    }
}
