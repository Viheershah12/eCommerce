namespace Management;

public static class ManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = "Management";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Management";
}
