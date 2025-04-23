using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Management.MongoDB;

[ConnectionStringName(ManagementDbProperties.ConnectionStringName)]
public interface IManagementMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
