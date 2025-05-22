using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Inventory.Dtos.Inventory;
using Inventory.Dtos.StockMovement;
using Inventory.Interfaces;
using Inventory.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;
using Abp.eCommerce.Enums;
using Inventory.Web.Models.StockMovement;
using Abp.eCommerce.Helpers;

namespace Inventory.Web.Pages.StockMovement
{
    public class EditModel : InventoryPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel StockMovement { get; set; } = new();

        [BindProperty]
        public StockMovementDropdownModel Dropdown { get; set; } = new();

        private readonly INotificationAppService _notificationAppService;
        private readonly IStockMovementAppService _stockMovementAppService;
        private readonly IStockBalanceAppService _stockBalanceAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        #endregion

        #region CTOR
        public EditModel(
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
        public class EditViewModel : StockMovementViewModel
        {

        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid id)
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

                var dto = await _stockMovementAppService.GetAsync(id);
                StockMovement = ObjectMapper.Map<CreateUpdateStockMovementDto, EditViewModel>(dto);

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
    }
}
