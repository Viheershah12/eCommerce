using Abp.eCommerce.Enums;
using Abp.eCommerce.Helpers;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using Order.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;
using static Order.Web.Models.Order.OrderViewModel;

namespace Order.Web.Pages.Order
{
    public class AddOrderNoteModalModel : OrderPageModel
    {
        #region Fields
        [BindProperty]
        public CreateViewModel OrderNote { get; set; } = new CreateViewModel();

        [BindProperty]
        public Guid OrderId { get; set; }

        [BindProperty]
        public List<SelectListItem> OrderNoteTypeDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        #endregion

        #region CTOR
        public AddOrderNoteModalModel(
            INotificationAppService notificationAppService,
            IOrderTransactionAppService orderTransactionAppService,
            IStringLocalizer<eCommerceResource> localizer
        )
        {
            _notificationAppService = notificationAppService;
            _orderTransactionAppService = orderTransactionAppService;
            _localizer = localizer;
        }
        #endregion

        #region Model
        public class CreateViewModel : OrderNoteViewModel
        {

        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid orderId)
        {
            try
            {
                OrderId = orderId;

                OrderNoteTypeDropdown = EnumHelper.ToSelectionList<OrderNoteType>(_localizer).Select(x => new SelectListItem
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
                var dto = ObjectMapper.Map<CreateViewModel, CreateUpdateOrderNoteDto>(OrderNote);

                dto.Id = Guid.NewGuid();
                dto.OrderId = OrderId;
                dto.CreatorId = CurrentUser.Id;
                dto.CreatorName = CurrentUser.Name + " " + CurrentUser.SurName;
                dto.CreationTime = DateTime.Now;

                await _orderTransactionAppService.CreateOrderNoteAsync(dto);
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
