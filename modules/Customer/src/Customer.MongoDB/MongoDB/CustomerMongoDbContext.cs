using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Customer.MongoDB;

[ConnectionStringName(CustomerDbProperties.ConnectionStringName)]
public class CustomerMongoDbContext : AbpMongoDbContext, ICustomerMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCustomer();
    }
}
