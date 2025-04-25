using Volo.Abp.Reflection;

namespace Management.Permissions;

public class ManagementPermissions
{
    public const string GroupName = "Management";

    public static class ContentManagement
    {
        public const string Default = GroupName + ".ContentManagement";
        public const string List = Default + ".List";
        public const string Create = Default + ".Create";
        public const string View = Default + ".View";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ManagementPermissions));
    }
}
