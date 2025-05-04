using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Order.Dtos.WishList
{
    public class WishListDto : BaseUserIdModel
    {
        public string Username { get; set; } = string.Empty;

        public int WishListCount { get; set; }  
    }

    public class GetWishListListingDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }
}
