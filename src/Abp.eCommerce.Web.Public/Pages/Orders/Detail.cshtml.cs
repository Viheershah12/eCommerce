using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using Abp.eCommerce.Web.Public.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using PaymentTransactions.Dtos.OrderTransaction;
using PaymentTransactions.Interfaces;
using System;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Abp.eCommerce.Web.Public.Pages.Orders
{
    public class DetailModel : eCommerceWebPublicPageModel
    {
        #region Fields
        [BindProperty]
        public OrderDetailViewModel Order { get; set; }

        [BindProperty]
        public PaymentDetailViewModel PaymentDetail { get; set; }

        private readonly INotificationAppService _notificationAppService;
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        private readonly IOrderDetailAppService _orderDetailAppService;
        #endregion

        #region CTOR
        public DetailModel(
            INotificationAppService notificationAppService,
            IOrderTransactionAppService orderTransactionAppService,
            IOrderDetailAppService orderDetailAppService
        )
        {
            _notificationAppService = notificationAppService;
            _orderTransactionAppService = orderTransactionAppService;
            _orderDetailAppService = orderDetailAppService;
        }
        #endregion

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var order = await _orderTransactionAppService.GetAsync(id);
                Order = ObjectMapper.Map<CreateUpdateOrderDto, OrderDetailViewModel>(order);

                var paymentTransaactions = await _orderDetailAppService.GetOrderPaymentDetailAsync(order.Id);
                PaymentDetail = ObjectMapper.Map<GetOrderPaymentDetailDto, PaymentDetailViewModel>(paymentTransaactions);

                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors); 
                return Redirect("/Orders/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/Orders/");
            }
        }
    }
}
