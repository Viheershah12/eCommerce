using Abp.eCommerce.Models;
using Product.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Product.Dtos.ProductCategory
{
    public class GetProductCategoryListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }

    public class ProductCategoryDto : BaseIdModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public MediaDto? CategoryMedia { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
