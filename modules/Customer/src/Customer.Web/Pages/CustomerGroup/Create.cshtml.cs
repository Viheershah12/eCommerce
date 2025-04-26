using Abp.eCommerce.Web.Common.Interfaces;
using Customer.Dtos.CustomerGroup;
using Customer.Interfaces;
using Customer.Web.Models.Common;
using Customer.Web.Models.CustomerGroup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Customer.Web.Pages.CustomerGroup
{
    public class CreateModel : CustomerPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel CustomerGroup { get; set; } = new CreateViewModel();

        private readonly INotificationAppService _notificationAppService;
        private readonly ICustomerGroupAppService _customerGroupAppService;
        #endregion

        #region CTOR
        public CreateModel(
            INotificationAppService notificationAppService,
            ICustomerGroupAppService customerGroupAppService
        )
        {
            _notificationAppService = notificationAppService;
            _customerGroupAppService = customerGroupAppService;
        }
        #endregion

        #region Model
        public class CreateViewModel : CustomerGroupViewModel
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
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateCustomerGroupDto>(CustomerGroup);
                
                var id = await _customerGroupAppService.CreateAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyAdded"]);

                return Redirect($"/CustomerGroup/Edit?id={id}");
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
    }
}
