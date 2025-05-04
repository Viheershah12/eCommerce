using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Order.Dtos.ShoppingCart
{
    public class ShoppingCartDto : BaseUserIdModel
    {
        public string Username { get; set; } = string.Empty;

        public int CartCount { get; set; }      
    }

    public class GetShoppingCartListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }
}
