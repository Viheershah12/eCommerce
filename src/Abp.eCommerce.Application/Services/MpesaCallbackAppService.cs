using Abp.eCommerce.Dtos.MpesaCallback;
using Abp.eCommerce.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Services
{
    public class MpesaCallbackAppService : eCommerceAppService, IMpesaCallbackAppService
    {
        public async Task HandleStkCallbackAsync(MpesaStkCallbackDto input)
        {
            var stk = input.Body?.StkCallback;
            if (stk == null)
            {
                Logger.LogWarning("Empty STK callback received.");
                return;
            }

            Logger.LogInformation("Received STK Callback: {@Callback}", stk);

            // Example: Check success and extract transaction ID
            if (stk.ResultCode == 0)
            {
                var metadata = stk.CallbackMetadata?.Item;

                var mpesaReceiptNumber = metadata?.FirstOrDefault(i => i.Name == "MpesaReceiptNumber")?.Value?.ToString();
                var amount = metadata?.FirstOrDefault(i => i.Name == "Amount")?.Value?.ToString();
                var phone = metadata?.FirstOrDefault(i => i.Name == "PhoneNumber")?.Value?.ToString();

                // TODO: Mark order as paid in database (based on phone or metadata)
                Logger.LogInformation("Payment Success: Receipt {Receipt}, Amount {Amount}, Phone {Phone}",
                    mpesaReceiptNumber, amount, phone);
            }
            else
            {
                Logger.LogWarning("Payment failed. ResultDesc: {ResultDesc}", stk.ResultDesc);
            }

            await Task.CompletedTask;
        }
    }
}
