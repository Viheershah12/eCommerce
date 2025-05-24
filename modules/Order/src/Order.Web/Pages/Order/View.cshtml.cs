using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using Order.Web.Models.Common;
using Order.Web.Models.Order;
using System;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Order.Web.Pages.Order
{
    public class ViewModel : OrderPageModel
    {
        #region Fields
        [BindProperty]
        public OrderViewModel Order { get; set; } = new OrderViewModel();

        public string OrderNotes { get; set; }

        private readonly INotificationAppService _notificationAppService;
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        #endregion

        #region CTOR
        public ViewModel(
            INotificationAppService notificationAppService,
            IOrderTransactionAppService orderTransactionAppService
        ) 
        {
            _notificationAppService = notificationAppService;
            _orderTransactionAppService = orderTransactionAppService; 
        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var order = await _orderTransactionAppService.GetOrderDetailAsync(id);
                Order = ObjectMapper.Map<OrderDetailDto, OrderViewModel>(order);

                OrderNotes = JsonConvert.SerializeObject(Order.OrderNotes);

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
