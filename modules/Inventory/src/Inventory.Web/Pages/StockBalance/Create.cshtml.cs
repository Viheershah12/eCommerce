using Abp.eCommerce.Helpers;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Inventory.Dtos.Inventory;
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
using Volo.Abp;
using Volo.Abp.Validation;

namespace Inventory.Web.Pages.StockBalance
{
    public class CreateModel : InventoryPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel StockBalance { get; set; } = new CreateViewModel();

        [BindProperty]
        public List<SelectListItem> ProductDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        private readonly IProductAppService _productAppService;
        private readonly IStockBalanceAppService _stockBalanceAppService;
        #endregion

        #region CTOR
        public CreateModel(
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
        public class CreateViewModel : StockBalanceViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync()
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

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/StockBalance");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/StockBalance");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateStockBalanceDto>(StockBalance);
                
                // Get Product
                var product = await _productAppService.GetAsync(dto.ProductId);
                dto.ProductName = product.Name;

                var id = await _stockBalanceAppService.CreateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

                return Redirect($"/StockBalance/Edit?id={id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/StockBalance/Create");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message, ex.GetLogLevel().ToString());
                return Redirect("/StockBalance/Create");
            }
        }
    }
}
