

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Product.ProductTag
{
    public interface IProductTagRepository : IRepository<Models.ProductTag, Guid>
    {
        Task<List<Models.ProductTag>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null);
    }
}
