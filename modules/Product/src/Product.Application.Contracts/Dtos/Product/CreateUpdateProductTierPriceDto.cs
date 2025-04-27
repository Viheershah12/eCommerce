
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dtos.Product
{
    public class CreateUpdateProductTierPriceDto : BaseIdModel
    {
        public Guid ProductId { get; set; }

        public Guid CustomerGroupId { get; set; }

        public string? CustomerGroup { get; set; }

        public decimal Price { get; set; }

        public int? MinimumQuantity { get; set; }
    }

    public class DeleteTierPriceDto : BaseIdModel
    {
        public Guid ProductId { get; set; }
    }

    public class GetTierPriceDto : BaseIdModel
    {
        public Guid ProductId { get; set; }
    }

    public class TierPriceDto : BaseIdModel
    {
        public Guid CustomerGroupId { get; set; }

        public string? CustomerGroup { get; set; }

        public decimal Price { get; set; }

        public int? MinimumQuantity { get; set; }
    }
}
