using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Product.Dtos.Product
{
    public class GetProductListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }

    public class GetProductCategoryListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }

        public Guid? Category { get; set; }
    }

    public class ProductDto : BaseIdModel   
    {
        public string Name { get; set; }

        public string? SKU { get; set; }

        public string? Description { get; set; }

        public bool IsPublished { get; set; }

        public bool IsNew { get; set; }
    }

    public class StoreProductDto : ProductDto
    {
        public decimal? Price { get; set; }

        public List<UserFileDto>? Media { get; set; }

        public Guid Category { get; set; }
    }

    public class DeleteProductMedia : BaseIdModel
    {
        public Guid FileId { get; set; }
    }
}
