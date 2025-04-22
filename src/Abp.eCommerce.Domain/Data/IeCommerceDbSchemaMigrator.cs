using System.Threading.Tasks;

namespace Abp.eCommerce.Data;

public interface IeCommerceDbSchemaMigrator
{
    Task MigrateAsync();
}
