using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using Abp.eCommerce.Web.Public.Models.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        #endregion

        #region CTOR
        public ProductModel(
            INotificationAppService notificationAppService,
            IProductAppService productAppService
        )
        {
            _notificationAppService = notificationAppService;
            _productAppService = productAppService;     
        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var product = await _productAppService.GetAsync(id);
                Product = ObjectMapper.Map<CreateUpdateProductDto, ProductViewModel>(product);

                // Similar Products
                var similarProducts = await _productAppService.GetSimilarProductAsync(product.ProductTags.Select(x => x.Id).ToList());
                similarProducts = similarProducts.Where(x => x.Id != id).ToList();

                SimilarProduct = ObjectMapper.Map<List<StoreProductDto>, List<ProductItemViewModel>>(similarProducts);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Page();
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Page();
            }
        }
    }
}
