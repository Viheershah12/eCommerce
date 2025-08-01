﻿using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Order.Models
{
    public class ShoppingCart : BaseFullAuditedAggregateRootUserModel
    {
        public string Username { get; set; } = string.Empty;

        public List<CartItem> Items { get; set; } = [];

        public partial class CartItem : BaseIdModel
        {
            public Guid ProductId { get; set; }

            public int Quantity { get; set; }

            public DateTime AddedOn { get; set; }

            public DateTime UpdatedOn { get; set; }
        }
    }
}
