namespace PaymentTransactions;

public static class PaymentTransactionsDbProperties
{
    public static string DbTablePrefix { get; set; } = "PaymentTransactions";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "PaymentTransactions";
}
