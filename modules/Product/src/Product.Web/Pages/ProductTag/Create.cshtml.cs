using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Dtos.ProductTag;
using Product.Interfaces;
using Product.Web.Models.ProductTag;
using System;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Product.Web.Pages.ProductTag
{
    public class CreateModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel ProductTag { get; set; } = new CreateViewModel();

        private readonly INotificationAppService _notificationAppService;
        private readonly IProductTagAppService _productTagAppService;
        #endregion

        #region CTOR
        public CreateModel(
            INotificationAppService notificationAppService,
            IProductTagAppService productTagAppService
        )
        {
            _notificationAppService = notificationAppService;   
            _productTagAppService = productTagAppService;
        }
        #endregion

        #region Model
        public class CreateViewModel : ProductTagViewModel
        {

        }
        #endregion 

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateProductTagDto>(ProductTag);

                var id = await _productTagAppService.CreateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

                return Redirect($"/ProductTag/Edit?id={id}");
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
    }
}
