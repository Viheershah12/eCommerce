using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Product.Dtos.ProductTag
{
    public class GetProductTagListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }

    public class ProductTagDto : BaseIdModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
