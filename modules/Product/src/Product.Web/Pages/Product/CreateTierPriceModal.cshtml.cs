using Abp.eCommerce.Helpers;
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
using Volo.Abp.Validation;

namespace Product.Web.Pages.Product
{
    public class CreateTierPriceModalModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel TierPrice { get; set; } = new CreateViewModel();

        [BindProperty]
        public List<SelectListItem> CustomerGroupDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IProductAppService _productAppService;
        private readonly Customer.Interfaces.ICommonAppService _customerCommonAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        #endregion

        #region CTOR
        public CreateTierPriceModalModel(
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
        public class CreateViewModel : ProductTierPriceViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                TierPrice.ProductId = id;

                var dropdown = await _customerCommonAppService.GetCustomerGroupDropdownAsync();
                CustomerGroupDropdown = dropdown.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

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
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateProductTierPriceDto>(TierPrice);
                
                await _productAppService.CreateTierPriceAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

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
