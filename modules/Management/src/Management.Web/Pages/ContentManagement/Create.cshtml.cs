using Abp.eCommerce.Enums;
using Abp.eCommerce.Helpers;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Management.Dtos.Content;
using Management.Interfaces;
using Management.Web.Models.Common;
using Management.Web.Models.ContentManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Management.Web.Pages.ContentManagement
{
    public class CreateModel : ManagementPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel Content { get; set; }

        [BindProperty]
        public List<SelectListItem> ContentTypeDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IContentAppService _contentAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        #endregion

        #region CTOR
        public CreateModel(
            INotificationAppService notificationAppService,
            IContentAppService contentAppService,
            IStringLocalizer<eCommerceResource> localizer
        )
        {
            _notificationAppService = notificationAppService;
            _contentAppService = contentAppService;
            _localizer = localizer;
        }
        #endregion

        #region Model
        public class CreateViewModel : ContentViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var contentTypeList = EnumHelper.ToSelectionList<ContentType>(_localizer);
                ContentTypeDropdown = contentTypeList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ContentManagement/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ContentManagement/");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateContentDto>(Content);

                var contentId = await _contentAppService.CreateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

                return Redirect($"/ContentManagement/Edit?id={contentId}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ContentManagement/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ContentManagement/");
            }
        }
    }
}
