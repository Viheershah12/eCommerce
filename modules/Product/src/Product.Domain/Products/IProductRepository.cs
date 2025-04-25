using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Product.Products
{
    public interface IProductRepository : IRepository<Models.Product, Guid>
    {
        Task<List<Models.Product>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null);
    }
}
