using Volo.Abp.Reflection;

namespace Customer.Permissions;

public class CustomerPermissions
{
    public const string GroupName = "Customer";

    public static class CustomerList
    {
        public const string Default = GroupName + ".CustomerList";
        public const string List = Default + ".List";
        public const string Create = Default + ".Create";
        public const string View = Default + ".View";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class CustomerGroup
    {
        public const string Default = GroupName + ".CustomerGroup";
        public const string List = Default + ".List";
        public const string Create = Default + ".Create";
        public const string View = Default + ".View";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CustomerPermissions));
    }
}
