using Product.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Product.Permissions;

public class ProductPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var productGroup = context.AddGroup(ProductPermissions.GroupName, L("Permission:Product"));

        var product = productGroup.AddPermission(ProductPermissions.Product.Default, L("Permissions:Product"), multiTenancySide: MultiTenancySides.Both);
        product.AddChild(ProductPermissions.Product.List, L("Permissions:Product.List"), multiTenancySide: MultiTenancySides.Both);
        product.AddChild(ProductPermissions.Product.View, L("Permissions:Product.View"), multiTenancySide: MultiTenancySides.Both);
        product.AddChild(ProductPermissions.Product.Create, L("Permissions:Product.Create"), multiTenancySide: MultiTenancySides.Both);
        product.AddChild(ProductPermissions.Product.Edit, L("Permissions:Product.Edit"), multiTenancySide: MultiTenancySides.Both);
        product.AddChild(ProductPermissions.Product.Delete, L("Permissions:Product.Delete"), multiTenancySide: MultiTenancySides.Both);

        var productCategory = productGroup.AddPermission(ProductPermissions.ProductCategory.Default, L("Permissions:ProductCategory"), multiTenancySide: MultiTenancySides.Both);
        productCategory.AddChild(ProductPermissions.ProductCategory.List, L("Permissions:ProductCategory.List"), multiTenancySide: MultiTenancySides.Both);
        productCategory.AddChild(ProductPermissions.ProductCategory.View, L("Permissions:ProductCategory.View"), multiTenancySide: MultiTenancySides.Both);
        productCategory.AddChild(ProductPermissions.ProductCategory.Create, L("Permissions:ProductCategory.Create"), multiTenancySide: MultiTenancySides.Both);
        productCategory.AddChild(ProductPermissions.ProductCategory.Edit, L("Permissions:ProductCategory.Edit"), multiTenancySide: MultiTenancySides.Both);
        productCategory.AddChild(ProductPermissions.ProductCategory.Delete, L("Permissions:ProductCategory.Delete"), multiTenancySide: MultiTenancySides.Both);

        var productTag = productGroup.AddPermission(ProductPermissions.ProductTag.Default, L("Permissions:ProductTag"), multiTenancySide: MultiTenancySides.Both);
        productTag.AddChild(ProductPermissions.ProductTag.List, L("Permissions:ProductTag.List"), multiTenancySide: MultiTenancySides.Both);
        productTag.AddChild(ProductPermissions.ProductTag.View, L("Permissions:ProductTag.View"), multiTenancySide: MultiTenancySides.Both);
        productTag.AddChild(ProductPermissions.ProductTag.Create, L("Permissions:ProductTag.Create"), multiTenancySide: MultiTenancySides.Both);
        productTag.AddChild(ProductPermissions.ProductTag.Edit, L("Permissions:ProductTag.Edit"), multiTenancySide: MultiTenancySides.Both);
        productTag.AddChild(ProductPermissions.ProductTag.Delete, L("Permissions:ProductTag.Delete"), multiTenancySide: MultiTenancySides.Both);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProductResource>(name);
    }
}
