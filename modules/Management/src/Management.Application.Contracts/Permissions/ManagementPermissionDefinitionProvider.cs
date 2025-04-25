using Management.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Management.Permissions;

public class ManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var managementGroup = context.AddGroup(ManagementPermissions.GroupName, L("Permission:Management"));

        var contentManagement = managementGroup.AddPermission(ManagementPermissions.ContentManagement.Default, L("Permissions:ContentManagement"), multiTenancySide: MultiTenancySides.Both);
        contentManagement.AddChild(ManagementPermissions.ContentManagement.List, L("Permissions:ContentManagement.List"), multiTenancySide: MultiTenancySides.Both);
        contentManagement.AddChild(ManagementPermissions.ContentManagement.View, L("Permissions:ContentManagement.View"), multiTenancySide: MultiTenancySides.Both);
        contentManagement.AddChild(ManagementPermissions.ContentManagement.Create, L("Permissions:ContentManagement.Create"), multiTenancySide: MultiTenancySides.Both);
        contentManagement.AddChild(ManagementPermissions.ContentManagement.Edit, L("Permissions:ContentManagement.Edit"), multiTenancySide: MultiTenancySides.Both);
        contentManagement.AddChild(ManagementPermissions.ContentManagement.Delete, L("Permissions:ContentManagement.Delete"), multiTenancySide: MultiTenancySides.Both);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ManagementResource>(name);
    }
}
