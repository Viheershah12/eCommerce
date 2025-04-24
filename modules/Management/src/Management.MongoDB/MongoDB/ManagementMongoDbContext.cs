using Management.Models;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Management.MongoDB;

[ConnectionStringName(ManagementDbProperties.ConnectionStringName)]
public class ManagementMongoDbContext : AbpMongoDbContext, IManagementMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<File> File => Collection<File>();   

    public IMongoCollection<Content> Content => Collection<Content>();  

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureManagement();
    }
}
