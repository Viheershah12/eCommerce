using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Inventory.Interfaces;
using Inventory.Web.Models.Common;
using Inventory.Web.Models.StockBalance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Product.Dtos.Product;
using Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Validation;
using Inventory.Dtos.Inventory;
using Volo.Abp.ObjectMapping;
using Abp.eCommerce.Helpers;

namespace Inventory.Web.Pages.StockBalance
{
    public class EditModel : InventoryPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel StockBalance { get; set; } = new EditViewModel();

        [BindProperty]
        public List<SelectListItem> ProductDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        private readonly IProductAppService _productAppService;
        private readonly IStockBalanceAppService _stockBalanceAppService;
        #endregion

        #region CTOR
        public EditModel(
            INotificationAppService notificationAppService,
            IStringLocalizer<eCommerceResource> localizer,
            IProductAppService productAppService,
            IStockBalanceAppService stockBalanceAppService
        )
        {
            _notificationAppService = notificationAppService;
            _localizer = localizer;
            _productAppService = productAppService;
            _stockBalanceAppService = stockBalanceAppService;
        }
        #endregion

        #region Model
        public class EditViewModel : StockBalanceViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var products = await _productAppService.GetListAsync(new GetProductListDto
                {
                    MaxResultCount = 1000
                });

                ProductDropdown = products.Items.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

                var stockBalance = await _stockBalanceAppService.GetAsync(id);
                StockBalance = ObjectMapper.Map<CreateUpdateStockBalanceDto, EditViewModel>(stockBalance);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/StockBalance");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message, ex.GetLogLevel().ToString());
                return Redirect("/StockBalance");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<EditViewModel, CreateUpdateStockBalanceDto>(StockBalance);

                // Get Product
                var product = await _productAppService.GetAsync(dto.ProductId);
                dto.ProductName = product.Name;

                await _stockBalanceAppService.UpdateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyUpdated"]);

                return Redirect($"/StockBalance/Edit?id={StockBalance.Id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect($"/StockBalance/Edit?id={StockBalance.Id}");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message, ex.GetLogLevel().ToString());
                return Redirect($"/StockBalance/Edit?id={StockBalance.Id}");
            }
        }
    }
}
