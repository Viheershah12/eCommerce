using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Dtos.WishList
{
    public class CreateUpdateWishListDto : BaseUserIdModel
    {
        public string Username { get; set; }

        public List<WishListItemDto> Items { get; set; } = [];

        public partial class WishListItemDto : BaseIdModel
        {
            public Guid ProductId { get; set; }

            public DateTime AddedOn { get; set; }
        }
    }

    public class CreateUpdateWishlistItemDto : BaseIdModel
    {
        public Guid ProductId { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
