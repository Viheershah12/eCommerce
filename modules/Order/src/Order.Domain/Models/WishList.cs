using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Models
{
    public class WishList : BaseFullAuditedAggregateRootUserModel
    {
        public string Username { get; set; } = string.Empty;

        public List<WishlistItem> Items { get; set; } = [];

        public class WishlistItem : BaseIdModel
        {
            public Guid ProductId { get; set; }

            public DateTime AddedOn { get; set; }
        }
    }
}
