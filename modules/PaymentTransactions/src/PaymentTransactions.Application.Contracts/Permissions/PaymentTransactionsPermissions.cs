using Volo.Abp.Reflection;

namespace PaymentTransactions.Permissions;

public class PaymentTransactionsPermissions
{
    public const string GroupName = "PaymentTransactions";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PaymentTransactionsPermissions));
    }
}
