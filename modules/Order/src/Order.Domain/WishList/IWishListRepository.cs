using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Order.WishList
{
    public interface IWishListRepository : IRepository<Models.WishList, Guid>
    {
        Task<List<Models.WishList>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null, Guid? userId = null);
    }
}
