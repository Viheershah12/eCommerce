using Abp.eCommerce.Web.Public.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Abp.eCommerce.Web.Public.Pages.ShoppingCart
{
    public class PendingModel : eCommerceWebPublicPageModel
    {
        [BindProperty]
        public Guid TransactionId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid transactionId)
        {
            TransactionId = transactionId;
            return Page();
        }
    }
}
