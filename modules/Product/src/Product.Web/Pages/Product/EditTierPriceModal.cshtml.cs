using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Product.Dtos.Product;
using Product.Interfaces;
using Product.Web.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;
using Abp.eCommerce.Helpers;

namespace Product.Web.Pages.Product
{
    public class EditTierPriceModalModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel TierPrice { get; set; } = new EditViewModel();

        [BindProperty]
        public List<SelectListItem> CustomerGroupDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IProductAppService _productAppService;
        private readonly Customer.Interfaces.ICommonAppService _customerCommonAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        #endregion

        #region CTOR
        public EditTierPriceModalModel(
            INotificationAppService notificationAppService,
            IProductAppService productAppService,
            Customer.Interfaces.ICommonAppService customerCommonAppService,
            IStringLocalizer<eCommerceResource> localizer
        )
        {
            _notificationAppService = notificationAppService;
            _productAppService = productAppService;
            _customerCommonAppService = customerCommonAppService;
            _localizer = localizer;
        }
        #endregion

        #region Model
        public class EditViewModel : ProductTierPriceViewModel
        {

        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid id, Guid productId)
        {
            try
            {
                // Dropdown
                var dropdown = await _customerCommonAppService.GetCustomerGroupDropdownAsync();
                CustomerGroupDropdown = dropdown.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

                // Tier Price
                var tierPrice = await _productAppService.GetTierPriceAsync(new GetTierPriceDto
                {
                    Id = id,
                    ProductId = productId
                });

                TierPrice = ObjectMapper.Map<TierPriceDto, EditViewModel>(tierPrice);
                TierPrice.ProductId = productId;

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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<EditViewModel, CreateUpdateProductTierPriceDto>(TierPrice);

                await _productAppService.UpdateTierPriceAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyUpdated"]);

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
