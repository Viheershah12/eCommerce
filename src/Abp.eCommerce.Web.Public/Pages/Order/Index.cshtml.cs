using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using Abp.eCommerce.Web.Public.Models.Order;
using Abp.eCommerce.Web.Public.Models.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using Product.Dtos.Product;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Abp.eCommerce.Web.Public.Pages.Orders
{
    public class IndexModel : eCommerceWebPublicPageModel
    {
        #region Fields
        [BindProperty]
        public BasePagedModel<OrderItemViewModel> Orders { get; set; }

        [BindProperty]
        public OrderStatus? SelectedStatus { get; set; } = null;

        private readonly IOrderTransactionAppService _orderTransactionAppService;
        private readonly INotificationAppService _notificationAppService;
        #endregion

        #region CTOR
        public IndexModel(
            IOrderTransactionAppService orderTransactionAppService,
            INotificationAppService notificationAppService
        )
        {
            _orderTransactionAppService = orderTransactionAppService;
            _notificationAppService = notificationAppService;
        }
        #endregion

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var res = await _orderTransactionAppService.GetListAsync(new GetOrderListDto());
                Orders = ObjectMapper.Map<BasePagedModel<OrderDto>, BasePagedModel<OrderItemViewModel>>(res);

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

        public async Task<IActionResult> OnGetPaginateAsync(OrderPaginationModel model)
        {
            try
            {
                var res = await _orderTransactionAppService.GetListAsync(new GetOrderListDto
                {
                    MaxResultCount = model.MaxResultCount,
                    SkipCount = model.SkipCount,
                    Status = model.Status,
                });

                Orders = ObjectMapper.Map<BasePagedModel<OrderDto>, BasePagedModel<OrderItemViewModel>>(res);
                SelectedStatus = model.Status;

                return PartialView("_Table", (Orders, SelectedStatus));
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnGetToggleStatusAsync(OrderStatus? status)
        {
            try
            {
                var res = await _orderTransactionAppService.GetListAsync(new GetOrderListDto
                {
                    Status = status,    
                });

                Orders = ObjectMapper.Map<BasePagedModel<OrderDto>, BasePagedModel<OrderItemViewModel>>(res);
                SelectedStatus = status;

                return PartialView("_Table", (Orders, SelectedStatus));
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Page();
            }
        }
    }
}
