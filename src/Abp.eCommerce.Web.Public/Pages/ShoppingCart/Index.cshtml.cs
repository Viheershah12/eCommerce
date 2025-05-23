using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Order.Dtos.Common;
using Order.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Validation;

namespace Abp.eCommerce.Web.Public.Pages.ShoppingCart
{
    public class IndexModel : eCommerceWebPublicPageModel
    {
        #region Fields
        [BindProperty]
        public List<ShoppingItemDto> CartItems { get; set; } = [];
                
        public UserAddress? BillingAddress { get; set; }

        public UserAddress? ShippingAddress { get; set; }

        private readonly Order.Interfaces.ICommonAppService _commonAppService;
        private readonly INotificationAppService _notificationAppService;
        private readonly IShoppingCartAppService _shoppingCartAppService;
        private readonly ICustomerAppService _customerAppService;
        #endregion

        #region CTOR
        public IndexModel(
            Order.Interfaces.ICommonAppService commonAppService,
            INotificationAppService notificationAppService,
            IShoppingCartAppService shoppingCartAppService,
            ICustomerAppService customerAppService
        )
        {
            _commonAppService = commonAppService;
            _notificationAppService = notificationAppService;
            _shoppingCartAppService = shoppingCartAppService;
            _customerAppService = customerAppService;
        }
        #endregion 

        public async Task<IActionResult> OnGetAsync()
        {
            if (!CurrentUser.Id.HasValue)
                return Redirect("/Account/Login");

            var stats = await _commonAppService.GetStatisticsAsync(CurrentUser.Id.Value);
            CartItems = stats.ShoppingCartItems;

            if (CurrentUser.Id.HasValue)
            {
                var customer = await _customerAppService.GetAsync(CurrentUser.Id.Value);

                ShippingAddress = customer.ShippingAddress;
                BillingAddress = customer.BillingAddress;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            try
            {
                var cartItems = CartItems.Where(x => x.Id != Guid.Empty).ToList();
                return Redirect("/ShoppingCart/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ShoppingCart/");
            }
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid productId)
        {
            try
            {
                await _shoppingCartAppService.RemoveShoppingCartItemAsync(productId);
                _notificationAppService.ShowSuccessToastNotification(TempData, L["SuccessfullyDeleted"]);

                return Redirect("/ShoppingCart/");
            }
            catch (AbpValidationException ex)
            {
                _notificationAppService.ProcessValidationErrors(TempData, ex.ValidationErrors);
                return Redirect("/ShoppingCart/");
            }
            catch (Exception ex)
            {
                _notificationAppService.ShowErrorPopupNotification(TempData, ex.Message);
                return Redirect("/ShoppingCart/");
            }
        }
    }
}
