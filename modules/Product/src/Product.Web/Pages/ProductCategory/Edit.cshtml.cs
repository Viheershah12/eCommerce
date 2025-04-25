using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Common.Interfaces;
using Management.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Dtos.ProductCategory;
using Product.Interfaces;
using Product.Web.Models.ProductCategory;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Product.Web.Pages.ProductCategory
{
    public class EditModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel ProductCategory { get; set; } = new EditViewModel();

        private readonly INotificationAppService _notificationAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;
        private readonly IFileAppService _fileAppService;
        #endregion

        #region CTOR
        public EditModel(
            INotificationAppService notificationAppService,
            IProductCategoryAppService productCategoryAppService,
            IFileAppService fileAppService
        ) 
        { 
            _notificationAppService = notificationAppService;
            _productCategoryAppService = productCategoryAppService;
            _fileAppService = fileAppService;
        }
        #endregion

        #region Model
        public class EditViewModel : ProductCategoryViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var productCategory = await _productCategoryAppService.GetAsync(id);
                ProductCategory = ObjectMapper.Map<CreateUpdateProductCategoryDto, EditViewModel>(productCategory);

                return Page();
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<EditViewModel, CreateUpdateProductCategoryDto>(ProductCategory);

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

                await _productCategoryAppService.UpdateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyUpdated"]);

                return Redirect($"/ProductCategory/Edit?id={ProductCategory.Id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect($"/ProductCategory/Edit?id={ProductCategory.Id}");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect($"/ProductCategory/Edit?id={ProductCategory.Id}");
            }
        }

        public async Task<IActionResult> OnGetDownloadFile(Guid fileId, string fileName)
        {
            try
            {
                var fileResponse = await _fileAppService.DownloadFileByIdAsync(fileId);

                if (fileResponse.DownloadBinary == null)
                    return StatusCode(500, "No File Found");

                var memoryStream = new MemoryStream(fileResponse.DownloadBinary);
                memoryStream.Position = 0;

                return File(memoryStream, fileResponse.ContentType ?? "application/octet-stream", fileResponse.Filename);
            }
            catch (AbpValidationException ex)
            {
                return StatusCode(500, new { message = ex.Message, error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, error = ex.Message });
            }
        }
    }
}
