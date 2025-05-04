using Order.Dtos.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Order.ShoppingCart
{
    public class ShoppingCartManager : DomainService
    {
        #region Fields
        private readonly IShoppingCartRepository _shoppingCartRepository;
        #endregion

        #region CTOR
        public ShoppingCartManager(
            IShoppingCartRepository shoppingCartRepository    
        ) 
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        #endregion

        public async Task<(List<Models.ShoppingCart> items, int totalCount)> GetShoppingCartListing(GetShoppingCartListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.ShoppingCart.Username);

            var result = await _shoppingCartRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _shoppingCartRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Username.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
