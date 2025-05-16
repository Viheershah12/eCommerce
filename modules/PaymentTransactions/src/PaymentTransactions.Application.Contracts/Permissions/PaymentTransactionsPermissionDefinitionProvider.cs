using PaymentTransactions.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PaymentTransactions.Permissions;

public class PaymentTransactionsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PaymentTransactionsPermissions.GroupName, L("Permission:PaymentTransactions"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PaymentTransactionsResource>(name);
    }
}
