using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using Abp.eCommerce.Web.Public.Models.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Order.Dtos.ShoppingCart;
using Order.Dtos.WishList;
using Order.Interfaces;
using Product.Dtos.Product;
using Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Abp.eCommerce.Web.Public.Pages.Store
{
    public class ProductModel : eCommerceWebPublicPageModel
    {
        #region Fields
        [BindProperty]
        public ProductViewModel Product { get; set; } = new ProductViewModel();

        [BindProperty]
        public List<ProductItemViewModel> SimilarProduct { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IProductAppService _productAppService;
        private readonly IShoppingCartAppService _shoppingCartAppService;
        private readonly Order.Interfaces.ICommonAppService _orderCommonAppService;
        private readonly IWishListAppService _wishListAppService;
        #endregion

        #region CTOR
        public ProductModel(
            INotificationAppService notificationAppService,
            IProductAppService productAppService,
            IShoppingCartAppService shoppingCartAppService,
            Order.Interfaces.ICommonAppService orderCommonAppService,
            IWishListAppService wishListAppService  
        )
        {
            _notificationAppService = notificationAppService;
            _productAppService = productAppService;
            _shoppingCartAppService = shoppingCartAppService;
            _orderCommonAppService = orderCommonAppService;
            _wishListAppService = wishListAppService;
        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var product = await _productAppService.GetAsync(id);
                Product = ObjectMapper.Map<CreateUpdateProductDto, ProductViewModel>(product);

                // Similar Products
                var similarProducts = await _productAppService.GetProductSuggestionsAsync(id);
                SimilarProduct = ObjectMapper.Map<List<StoreProductDto>, List<ProductItemViewModel>>(similarProducts);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/Store");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/Store");
            }
        }
        
        public async Task<JsonResult> OnGetAddToCartAsync(AddToCartDto model)
        {
            try
            {
                if (CurrentUser.Id == null)
                    return new JsonResult(new { success = false, message = L["NotAuthenticated"] });

                var dto = new CreateUpdateShoppingCartItemDto
                {
                    Id = Guid.NewGuid(),
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    AddedOn = DateTime.Now
                };

                await _shoppingCartAppService.AddShoppingCartItemAsync(dto);

                // Return success and new cart data
                var updatedCart = await _orderCommonAppService.GetStatisticsAsync(CurrentUser.Id.Value); // Make sure this returns count/items
                return new JsonResult(new
                {
                    success = true,
                    cartCount = updatedCart.ShoppingCartCount,
                    cartItemsHtml = await RenderPartialViewToString("Pages/Store/_CartCanvasPartial.cshtml", updatedCart.ShoppingCartItems)
                });
            }
            catch (AbpValidationException ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public async Task<JsonResult> OnGetAddToWishlistAsync(Guid productId)
        {
            try
            {
                if (CurrentUser.Id == null)
                    return new JsonResult(new { success = false, message = L["NotAuthenticated"] });

                var dto = new CreateUpdateWishlistItemDto
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    AddedOn = DateTime.Now
                };

                await _wishListAppService.AddWishlistItemAsync(dto);

                // Return success and new wishlist data
                var updatedCart = await _orderCommonAppService.GetStatisticsAsync(CurrentUser.Id.Value);
                return new JsonResult(new
                {
                    success = true,
                    wishListCount = updatedCart.WishListCount,
                    wishListItemsHtml = await RenderPartialViewToString("Pages/Store/_WishlistCanvasPartial.cshtml", updatedCart.WishListItems)
                });
            }
            catch (AbpValidationException ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }
}
