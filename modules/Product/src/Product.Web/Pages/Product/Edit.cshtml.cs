using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Product.Interfaces;
using Product.Web.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Validation;
using Abp.eCommerce.Enums;
using Product.Dtos.Product;
using Volo.Abp.ObjectMapping;

namespace Product.Web.Pages.Product
{
    public class EditModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel Product { get; set; } = new EditViewModel();

        [BindProperty]
        public List<SelectListItem> ProductCategoryDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        private readonly IProductAppService _productAppService;
        #endregion

        #region CTOR
        public EditModel(
            INotificationAppService notificationAppService,
            ICommonAppService commonAppService,
            IStringLocalizer<eCommerceResource> localizer,
            IProductAppService productAppService
        )
        {
            _notificationAppService = notificationAppService;
            _commonAppService = commonAppService;
            _localizer = localizer;
            _productAppService = productAppService;
        }
        #endregion

        #region Model
        public class EditViewModel : ProductViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var dropdown = await _commonAppService.GetProductCategoryDropdownAsync();

                ProductCategoryDropdown = dropdown.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

                var product = await _productAppService.GetAsync(id);
                Product = ObjectMapper.Map<CreateUpdateProductDto, EditViewModel>(product);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/Product/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/Product/");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<EditViewModel, CreateUpdateProductDto>(Product);

                await _productAppService.UpdateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyUploaded"]);

                return Redirect($"/Product/Edit?id={Product.Id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect($"/Product/Edit?id={Product.Id}");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect($"/Product/Edit?id={Product.Id}");
            }
        }
    }
}
