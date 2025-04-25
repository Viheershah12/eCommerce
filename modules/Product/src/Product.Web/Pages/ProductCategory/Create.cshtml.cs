using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Dtos.ProductCategory;
using Product.Interfaces;
using Product.Web.Models.ProductCategory;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Product.Web.Pages.ProductCategory
{
    public class CreateModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel ProductCategory { get; set; } = new CreateViewModel();

        private readonly INotificationAppService _notificationAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;
        #endregion

        #region CTOR
        public CreateModel(
            INotificationAppService notificationAppService,
            IProductCategoryAppService productCategoryAppService
        )
        {
            _notificationAppService = notificationAppService;
            _productCategoryAppService = productCategoryAppService;
        }
        #endregion

        #region Model
        public class CreateViewModel : ProductCategoryViewModel
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
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateProductCategoryDto>(ProductCategory);

                if (ProductCategory.CategoryMedia != null)
                {
                    using var memoryStream = new MemoryStream();
                    await ProductCategory.CategoryMedia.CopyToAsync(memoryStream);

                    var fileDto = new UserFileDto
                    {
                        Filename = ProductCategory.CategoryMedia.FileName,
                        ContentType = ProductCategory.CategoryMedia.ContentType,
                        Extension = Path.GetExtension(ProductCategory.CategoryMedia.FileName),
                        DownloadBinary = memoryStream.ToArray()
                    };
                    dto.CategoryMedia = fileDto;    
                }

                var id = await _productCategoryAppService.CreateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

                return Redirect($"/ProductCategory/Edit?id={id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ProductCategory/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ProductCategory/");
            }
        }
    }
}
