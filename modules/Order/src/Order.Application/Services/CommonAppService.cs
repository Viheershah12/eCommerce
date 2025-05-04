using Order.Dtos.Common;
using Order.Dtos.ShoppingCart;
using Order.Dtos.WishList;
using Order.Interfaces;
using Order.ShoppingCart;
using Order.WishList;
using Product.Dtos.Product;
using Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Order.Services
{
    public class CommonAppService : OrderAppService, Interfaces.ICommonAppService
    {
        #region Fields
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IWishListRepository _wishListRepository;
        private readonly IProductAppService _productAppService; 
        #endregion

        #region CTOR
        public CommonAppService(
            IShoppingCartRepository shoppingCartRepository,
            IWishListRepository wishListRepository,
            IProductAppService productAppService    
        )
        {
            _shoppingCartRepository = shoppingCartRepository;
            _wishListRepository = wishListRepository;
            _productAppService = productAppService;
        }
        #endregion 

        public async Task<StatisticDto> GetStatisticsAsync(Guid userId)
        {
            try
            {
                var res = new StatisticDto();

                // Shopping Cart
                var shoppingCartQuerable = await _shoppingCartRepository.GetQueryableAsync();
                var shoppingCart = shoppingCartQuerable.FirstOrDefault(x => x.UserId == userId);

                if (shoppingCart != null)
                {
                    res.ShoppingCartCount = shoppingCart.Items.Count;

                    // Get Products
                    if (shoppingCart.Items.Count > 0)
                    {
                        var products = await _productAppService.GetProductByMultipleIdAsync(shoppingCart.Items.Select(x => x.ProductId).ToList());
                        var productList = ObjectMapper.Map<List<StoreProductDto>, List<ShoppingItemDto>>(products);

                        foreach (var prod in productList)
                        {
                            var cartItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == prod.Id);

                            if (cartItem != null)
                            {
                                prod.Quantity = cartItem.Quantity;
                                res.ShoppingCartItems.Add(prod);
                            }
                        }
                    }                    
                }

                // Wish List
                var wishListQuerable = await _wishListRepository.GetQueryableAsync();
                var wishList = wishListQuerable.FirstOrDefault(x => x.UserId == userId);

                if (wishList != null)
                {
                    res.WishListCount = wishList.Items.Count;

                    // Get Products
                    if (wishList.Items.Count > 0)
                    {
                        var products = await _productAppService.GetProductByMultipleIdAsync(wishList.Items.Select(x => x.ProductId).ToList());
                        res.WishListItems = products;
                    }                    
                }

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
