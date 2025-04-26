
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dtos.ProductTag
{
    public class CreateUpdateProductTagDto : BaseIdModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
