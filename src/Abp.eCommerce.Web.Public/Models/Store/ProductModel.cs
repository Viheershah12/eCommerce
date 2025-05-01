using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;

namespace Abp.eCommerce.Web.Public.Models.Store
{
    public class ProductModel : BaseIdModel
    {
        public string Name { get; set; }

        public string? SKU { get; set; }

        public string? Description { get; set; }

        public bool IsPublished { get; set; }

        public bool IsNew { get; set; }

        public decimal? Price { get; set; }

        public List<UserFileDto>? Media { get; set; }

        public Guid Category { get; set; }
    }

    public class ProductPaginationModel : BasePaginationModel
    {
        public Guid Category { get; set; }
    }
}
