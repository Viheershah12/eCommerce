using Abp.eCommerce.Web.Common.Interfaces;
using Inventory.Interfaces;
using Inventory.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Dtos.Product;
using System.Threading.Tasks;
using System;
using Volo.Abp.Validation;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inventory.Dtos.Inventory;
using System.Linq;
using Microsoft.Extensions.Localization;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Enums;
using Inventory.Dtos.StockMovement;
using Inventory.Web.Models.StockMovement;

namespace Inventory.Web.Pages.StockMovement
{
    public class CreateModel : InventoryPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel StockMovement { get; set; } = new();

        [BindProperty]
        public StockMovementDropdownModel Dropdown { get; set; } = new();

        private readonly INotificationAppService _notificationAppService;
        private readonly IStockMovementAppService _stockMovementAppService;
        private readonly IStockBalanceAppService _stockBalanceAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        #endregion

        #region CTOR
        public CreateModel(
            INotificationAppService notificationAppService,
            IStockMovementAppService stockMovementAppService,
            IStockBalanceAppService stockBalanceAppService,
            IStringLocalizer<eCommerceResource> localizer
        ) 
        {
            _notificationAppService = notificationAppService;
            _stockMovementAppService = stockMovementAppService;
            _stockBalanceAppService = stockBalanceAppService;
            _localizer = localizer;
        }
        #endregion

        #region Model
        public class CreateViewModel : StockMovementViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var products = await _stockBalanceAppService.GetListAsync(new GetStockBalanceListDto
                {
                    MaxResultCount = 1000
                });

                Dropdown.InventoryDropdown = products.Items.Select(x => new SelectListItem
                {
                    Text = x.ProductName,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

                Dropdown.StockMovementTypeDropdown = EnumHelper.ToSelectionList<StockMovementType>(_localizer).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/StockMovement");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message, ex.GetLogLevel().ToString());
                return Redirect("/StockMovement");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateStockMovementDto>(StockMovement);

                var id = await _stockMovementAppService.CreateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

                return Redirect($"/StockMovement/Edit?id={id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/StockMovement/Create");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message, ex.GetLogLevel().ToString());
                return Redirect("/StockMovement/Create");
            }
        }
    }
}
