using Abp.eCommerce.Web.Common.Interfaces;
using Customer.Dtos.CustomerGroup;
using Customer.Interfaces;
using Customer.Web.Models.Common;
using Customer.Web.Models.CustomerGroup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;

namespace Customer.Web.Pages.CustomerGroup
{
    public class EditModel : CustomerPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel CustomerGroup { get; set; } = new EditViewModel();

        private readonly INotificationAppService _notificationAppService;
        private readonly ICustomerGroupAppService _customerGroupAppService;
        #endregion

        #region CTOR
        public EditModel(
            INotificationAppService notificationAppService,
            ICustomerGroupAppService customerGroupAppService
        )
        {
            _notificationAppService = notificationAppService;
            _customerGroupAppService = customerGroupAppService;
        }
        #endregion

        #region Model
        public class EditViewModel : CustomerGroupViewModel
        {

        }
        #endregion 

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var customerGroup = await _customerGroupAppService.GetAsync(id);
                CustomerGroup = ObjectMapper.Map<CreateUpdateCustomerGroupDto, EditViewModel>(customerGroup);

                return Page();
            }
            catch (AbpValidationException ex) 
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/CustomerGroup/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/CustomerGroup/");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var dto = ObjectMapper.Map<EditViewModel, CreateUpdateCustomerGroupDto>(CustomerGroup);

                await _customerGroupAppService.UpdateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyUpdated"]);

                return Redirect($"/CustomerGroup/Edit?id={CustomerGroup.Id}");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect($"/CustomerGroup/Edit?id={CustomerGroup.Id}");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect($"/CustomerGroup/Edit?id={CustomerGroup.Id}");
            }
        }
    }
}
