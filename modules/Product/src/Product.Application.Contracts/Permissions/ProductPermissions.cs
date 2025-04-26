using Volo.Abp.Reflection;

namespace Product.Permissions;

public class ProductPermissions
{
    public const string GroupName = "Product";

    public static class Product
    {
        public const string Default = GroupName + ".Product";
        public const string List = Default + ".List";
        public const string Create = Default + ".Create";
        public const string View = Default + ".View";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class ProductCategory
    {
        public const string Default = GroupName + ".ProductCategory";
        public const string List = Default + ".List";
        public const string Create = Default + ".Create";
        public const string View = Default + ".View";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class ProductTag
    {
        public const string Default = GroupName + ".ProductTag";
        public const string List = Default + ".List";
        public const string Create = Default + ".Create";
        public const string View = Default + ".View";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProductPermissions));
    }
}
