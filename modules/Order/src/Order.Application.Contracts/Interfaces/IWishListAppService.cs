using Order.Dtos.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Order.Interfaces
{
    public interface IWishListAppService : IApplicationService  
    {
        Task<PagedResultDto<WishListDto>> GetListAsync(GetWishListListingDto dto);

        Task<Guid> CreateAsync(CreateUpdateWishListDto dto);

        Task<CreateUpdateWishListDto> GetAsync(Guid id);

        Task AddWishlistItemAsync(CreateUpdateWishlistItemDto dto);

        Task UpdateAsync(CreateUpdateWishListDto dto);

        Task DeleteAsync(Guid id);
    }
}
