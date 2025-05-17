using Abp.eCommerce.Dtos.Checkout;
using Abp.eCommerce.Dtos.Mpesa;
using Abp.eCommerce.Enums;
using Abp.eCommerce.Interfaces;
using Abp.eCommerce.Localization;
using Abp.eCommerce.Services;
using Abp.eCommerce.Web.Common.Interfaces;
using Abp.eCommerce.Web.Public.Models.Common;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Order.Dtos.Common;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using PaymentTransactions.Dtos.PaymentTransaction;
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
        private readonly ICheckoutAppService _checkoutAppService;
        private readonly IMpesaAppService _mpesaAppService;
        private readonly IShoppingCartAppService _shoppingCartAppService;

        public CheckoutModel( 
            IStringLocalizer<eCommerceResource> localizer,
            INotificationAppService notificationAppService,
            ICheckoutAppService checkoutAppService,
            IMpesaAppService mpesaAppService,
            IShoppingCartAppService shoppingCartAppService
        )
        {
            _localizer = localizer;
            _notificationAppService = notificationAppService;
            _checkoutAppService = checkoutAppService;
            _mpesaAppService = mpesaAppService;
            _shoppingCartAppService = shoppingCartAppService;
        }   

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!CurrentUser.IsAuthenticated || CurrentUser.Id == null || string.IsNullOrEmpty(CurrentUser.UserName))
                    return Redirect("/Account/Login");

                var order = new CreateUpdateOrderDto(CurrentUser.UserName)
                {
                    CustomerName = CurrentUser.UserName,
                    CustomerId = CurrentUser.Id.Value,
                    OrderItems = CartItems.Select(x => new CreateUpdateOrderDto.OrderItemDto
                    {
                        Id = Guid.NewGuid(),
                        ProductId = x.Id,
                        ProductName = x.Name,
                        Quantity = x.Quantity,
                        Price = x.Price ?? 0
                    }).ToList(),
                    Status = PaymentMethod == PaymentMethodEnum.CashOnDelivery ? OrderStatus.Processing : OrderStatus.Pending,
                    PaymentStatus = PaymentMethod == PaymentMethodEnum.CashOnDelivery ? PaymentStatus.Paid : PaymentStatus.Pending,
                    PaymentMethod = PaymentMethod == PaymentMethodEnum.CashOnDelivery ? PaymentMethod : null,
                    PaymentMethodSystemName = PaymentMethod == PaymentMethodEnum.CashOnDelivery ? PaymentMethod.ToString() : null,
                };

                var paymentTransaction = new CreateUpdatePaymentTransactionDto
                {
                    Amount = CartItems.Sum(x => x.Quantity * x.Price) ?? 0,
                    PaymentMethod = PaymentMethod,
                    Status = PaymentMethod == PaymentMethodEnum.CashOnDelivery ? PaymentTransactionStatus.PendingConfirmed : PaymentTransactionStatus.Pending,
                    PaymentDate = DateTime.Now, 
                };

                var checkoutDto = new CheckoutDto(order, paymentTransaction);
                var paymentTransactionId = await _checkoutAppService.CheckoutAsync(checkoutDto);

                // Handle STK Push if needed
                if (PaymentMethod == PaymentMethodEnum.MpesaStk)
                {
                    var phoneNumber = PhoneNumber; // Fetch from form or user profile

                    var stkInput = new MpesaStkPushRequestDto
                    {
                        PaymentTransactionId = paymentTransactionId,
                        PhoneNumber = phoneNumber,
                        Amount = paymentTransaction.Amount,
                        AccountReference = "eCommerce",
                        TransactionDescription = "eCommerce"
                    };

                    var stkResponse = await _mpesaAppService.InitiateSTKPushAsync(stkInput);
                    return RedirectToPage("/ShoppingCart/Pending", new { transactionId = paymentTransactionId });
                }

                // Delete Cart Item(s)
                await _shoppingCartAppService.DeleteShoppingCartItemsAsync(CartItems.Select(x => x.CartItemId).ToList());

                return Redirect("/Order/");
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
