using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Dtos.ProductTag;
using Product.Interfaces;
using Product.Web.Models.ProductTag;
using System;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;

namespace Product.Web.Pages.ProductTag
{
    public class EditModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel ProductTag { get; set; } = new EditViewModel();

        private readonly INotificationAppService _notificationAppService;
        private readonly IProductTagAppService _productTagAppService;
        #endregion

        #region CTOR
        public EditModel(
            INotificationAppService  notificationAppService,
            IProductTagAppService productTagAppService
        )
        {
            _notificationAppService = notificationAppService;
            _productTagAppService = productTagAppService;
        }
        #endregion

        #region Model
        public class EditViewModel : ProductTagViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var productTag = await _productTagAppService.GetAsync(id);
                ProductTag = ObjectMapper.Map<CreateUpdateProductTagDto, EditViewModel>(productTag);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/ProductTag/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ProductTag/");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<EditViewModel, CreateUpdateProductTagDto>(ProductTag);

                await _productTagAppService.UpdateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyUpdated"]);

                return Redirect($"/ProductTag/Edit?id={ProductTag.Id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect($"/ProductTag/Edit?id={ProductTag.Id}");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect($"/ProductTag/Edit?id={ProductTag.Id}");
            }
        }
    }
}
