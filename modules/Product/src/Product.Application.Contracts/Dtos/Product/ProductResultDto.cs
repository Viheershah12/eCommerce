using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dtos.Product
{
    public class ProductResultDto : BaseIdModel
    {
        public string Name { get; set; }
    }

    public class ProductSearchDto
    {
        public string Query { get; set; }

        public Dictionary<string, object> Filter { get; set; }
    }
}
