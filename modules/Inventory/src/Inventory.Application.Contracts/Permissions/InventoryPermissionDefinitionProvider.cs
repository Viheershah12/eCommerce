﻿using Inventory.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Inventory.Permissions;

public class InventoryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InventoryPermissions.GroupName, L("Permission:Inventory"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InventoryResource>(name);
    }
}
