using Customer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Customer.Permissions;

public class CustomerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var customerGroup = context.AddGroup(CustomerPermissions.GroupName, L("Permission:Customer"));

        var customerList = customerGroup.AddPermission(CustomerPermissions.CustomerList.Default, L("Permissions:CustomerList"), multiTenancySide: MultiTenancySides.Both);
        customerList.AddChild(CustomerPermissions.CustomerList.List, L("Permissions:CustomerList.List"), multiTenancySide: MultiTenancySides.Both);
        customerList.AddChild(CustomerPermissions.CustomerList.View, L("Permissions:CustomerList.View"), multiTenancySide: MultiTenancySides.Both);
        customerList.AddChild(CustomerPermissions.CustomerList.Create, L("Permissions:CustomerList.Create"), multiTenancySide: MultiTenancySides.Both);
        customerList.AddChild(CustomerPermissions.CustomerList.Edit, L("Permissions:CustomerList.Edit"), multiTenancySide: MultiTenancySides.Both);
        customerList.AddChild(CustomerPermissions.CustomerList.Delete, L("Permissions:CustomerList.Delete"), multiTenancySide: MultiTenancySides.Both);

        var customerGroupPermissions = customerGroup.AddPermission(CustomerPermissions.CustomerGroup.Default, L("Permissions:CustomerGroup"), multiTenancySide: MultiTenancySides.Both);
        customerGroupPermissions.AddChild(CustomerPermissions.CustomerGroup.List, L("Permissions:CustomerGroup.List"), multiTenancySide: MultiTenancySides.Both);
        customerGroupPermissions.AddChild(CustomerPermissions.CustomerGroup.View, L("Permissions:CustomerGroup.View"), multiTenancySide: MultiTenancySides.Both);
        customerGroupPermissions.AddChild(CustomerPermissions.CustomerGroup.Create, L("Permissions:CustomerGroup.Create"), multiTenancySide: MultiTenancySides.Both);
        customerGroupPermissions.AddChild(CustomerPermissions.CustomerGroup.Edit, L("Permissions:CustomerGroup.Edit"), multiTenancySide: MultiTenancySides.Both);
        customerGroupPermissions.AddChild(CustomerPermissions.CustomerGroup.Delete, L("Permissions:CustomerGroup.Delete"), multiTenancySide: MultiTenancySides.Both);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CustomerResource>(name);
    }
}
