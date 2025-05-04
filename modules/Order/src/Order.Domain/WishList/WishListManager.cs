using Order.Dtos.ShoppingCart;
using Order.Dtos.WishList;
using Order.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Order.WishList
{
    public class WishListManager : DomainService    
    {
        #region Fields
        private readonly IWishListRepository _wishListRepository;
        #endregion

        #region CTOR
        public WishListManager(
            IWishListRepository wishListRepository
        )
        {
            _wishListRepository = wishListRepository;
        }
        #endregion

        public async Task<(List<Models.WishList> items, int totalCount)> GetWishListListing(GetWishListListingDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.WishList.Username);

            var result = await _wishListRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _wishListRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Username.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
