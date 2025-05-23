using Volo.Abp.Identity;

namespace Abp.eCommerce;

public static class eCommerceConsts
{
    public const string DbTablePrefix = "App";
    public const string? DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;
}

public static class eCommerceCacheKeys
{
    public const string Profile = "UserProfile_{0}";
    public const string ShippingAddress = "ShippingAddress_{0}";
    public const string BillingAddress = "BillingAddress_{0}";
}
