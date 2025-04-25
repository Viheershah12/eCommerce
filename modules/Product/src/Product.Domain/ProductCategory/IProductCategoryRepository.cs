using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Product.ProductCategory
{
    public interface IProductCategoryRepository : IRepository<Models.ProductCategory, Guid>
    {
        Task<List<Models.ProductCategory>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null);
    }
}
