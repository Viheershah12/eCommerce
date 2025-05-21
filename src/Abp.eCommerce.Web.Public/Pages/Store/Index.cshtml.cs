using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using Abp.eCommerce.Web.Public.Models.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Dtos.Product;
using Product.Interfaces;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp.eCommerce.Web.Public.Pages.Store
{
    public class IndexModel : eCommerceWebPublicPageModel
    {
        #region Fields
        [BindProperty]
        public BasePagedModel<ProductItemViewModel> Products { get; set; }

        private readonly IProductAppService _productAppService;
        private readonly INotificationAppService _notificationAppService;
        #endregion

        #region CTOR
        public IndexModel(
            IProductAppService productAppService, 
            INotificationAppService notificationAppService)
        {
            _productAppService = productAppService;
            _notificationAppService = notificationAppService;
        }
        #endregion

        public async Task<IActionResult> OnGetAsync(PagedResultRequestDto dto)
        {
            try
            {
                var res = await _productAppService.GetListByCategoryAsync(new GetProductCategoryListDto
                {
                    MaxResultCount = 5
                });

                Products = ObjectMapper.Map<BasePagedModel<StoreProductDto>, BasePagedModel<ProductItemViewModel>>(res);
                return Page();
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnGetSwitchCategoryAsync(Guid categoryId)
        {
            try
            {
                var res = await _productAppService.GetListByCategoryAsync(new GetProductCategoryListDto
                {
                    Category = categoryId,
                    MaxResultCount = 5,
                });

                Products = ObjectMapper.Map<PagedResultDto<StoreProductDto>, BasePagedModel<ProductItemViewModel>>(res);
                return PartialView("_Table", Products);
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnGetPaginateAsync(ProductPaginationModel model)
        {
            try
            {
                var res = await _productAppService.GetListByCategoryAsync(new GetProductCategoryListDto
                {
                    Category = model.Category,
                    MaxResultCount = model.MaxResultCount,
                    SkipCount = model.SkipCount,
                });

                Products = ObjectMapper.Map<PagedResultDto<StoreProductDto>, BasePagedModel<ProductItemViewModel>>(res);
                Products.PageNumber = model.PageNumber;

                return PartialView("_Table", Products);
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Page();
            }
        }
    }
}
