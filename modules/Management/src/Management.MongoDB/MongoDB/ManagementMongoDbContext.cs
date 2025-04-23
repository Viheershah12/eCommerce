using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Management.MongoDB;

[ConnectionStringName(ManagementDbProperties.ConnectionStringName)]
public class ManagementMongoDbContext : AbpMongoDbContext, IManagementMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureManagement();
    }
}
