using Abp.eCommerce.Helpers;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Models;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Product.Web.Pages.Product
{
    public class CreateModel : ProductPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel Product { get; set; } = new CreateViewModel();

        [BindProperty]
        public List<SelectListItem> ProductCategoryDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        private readonly IProductAppService _productAppService;
        #endregion

        #region CTOR
        public CreateModel(
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
        public class CreateViewModel : ProductViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var dropdown = await _commonAppService.GetProductCategoryDropdownAsync();

                ProductCategoryDropdown = dropdown.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

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
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateProductDto>(Product);

                if (Product.Media != null)
                {
                    using var memoryStream = new MemoryStream();
                    dto.Media = []; 

                    foreach(var file in Product.Media)
                    {
                        await file.CopyToAsync(memoryStream);

                        var fileDto = new UserFileDto
                        {
                            Filename = file.FileName,
                            ContentType = file.ContentType,
                            Extension = Path.GetExtension(file.FileName),
                            DownloadBinary = memoryStream.ToArray()
                        };

                        dto.Media.Add(fileDto);
                    }
                }

                var id = await _productAppService.CreateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

                return Redirect($"/Product/Edit?id={id}");
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
    }
}
