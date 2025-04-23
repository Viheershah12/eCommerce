using Volo.Abp.Reflection;

namespace Management.Permissions;

public class ManagementPermissions
{
    public const string GroupName = "Management";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ManagementPermissions));
    }
}
