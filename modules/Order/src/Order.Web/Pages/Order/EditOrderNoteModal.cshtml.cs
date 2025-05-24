using Abp.eCommerce.Enums;
using Abp.eCommerce.Helpers;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Order.Interfaces;
using static Order.Web.Models.Order.OrderViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Validation;
using Order.Web.Models.Common;
using System.Linq;
using Order.Dtos.OrderTransaction;

namespace Order.Web.Pages.Order
{
    public class EditOrderNoteModalModel : OrderPageModel
    {
        #region Fields
        [BindProperty]
        public EditViewModel OrderNote { get; set; } = new EditViewModel();

        [BindProperty]
        public Guid OrderId { get; set; }

        [BindProperty]
        public List<SelectListItem> OrderNoteTypeDropdown { get; set; } = [];

        private readonly INotificationAppService _notificationAppService;
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        private readonly IStringLocalizer<eCommerceResource> _localizer;
        #endregion

        #region CTOR
        public EditOrderNoteModalModel(
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
        public class EditViewModel : OrderNoteViewModel
        {

        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid id, Guid orderId)
        {
            try
            {
                OrderId = orderId;

                OrderNoteTypeDropdown = EnumHelper.ToSelectionList<OrderNoteType>(_localizer).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList().AddDefaultValue(_localizer);

                var orderNote = await _orderTransactionAppService.GetOrderNoteAsync(new IdOrderIdModel
                {
                    Id = id,
                    OrderId = orderId
                });

                OrderNote = ObjectMapper.Map<CreateUpdateOrderNoteDto, EditViewModel>(orderNote);

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
                var dto = ObjectMapper.Map<EditViewModel, CreateUpdateOrderNoteDto>(OrderNote);
                dto.OrderId = OrderId;

                await _orderTransactionAppService.UpdateOrderNoteAsync(dto);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyUpdated"]); 

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
