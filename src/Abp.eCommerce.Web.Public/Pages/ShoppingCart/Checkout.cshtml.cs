using Abp.eCommerce.Enums;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Order.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Abp.eCommerce.Web.Public.Pages.ShoppingCart
{
    public class CheckoutModel : eCommerceWebPublicPageModel
    {
        [BindProperty]
        public List<ShoppingItemDto> CartItems { get; set; } = [];

        [BindProperty]
        public List<SelectListItem> PaymentMethodOptions { get; set; }

        [BindProperty]
        public PaymentMethodEnum PaymentMethod { get; set; }

        [BindProperty]
        public long PhoneNumber { get; set; }

        private readonly IStringLocalizer<eCommerceResource> _localizer;
        private readonly INotificationAppService _notificationAppService;

        public CheckoutModel( 
            IStringLocalizer<eCommerceResource> localizer,
            INotificationAppService notificationAppService
        )
        {
            _localizer = localizer;
            _notificationAppService = notificationAppService;
        }   

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {


                return Page();
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Page();
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message, ex.GetLogLevel().ToString());
                return Page();
            }
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            CartItems = CartItems.Where(x => x.Id != Guid.Empty).ToList();
            PaymentMethodOptions = EnumHelper.ToSelectionList<PaymentMethodEnum>(_localizer).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return Page();
        }
    }
}
