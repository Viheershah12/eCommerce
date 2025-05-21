using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Dtos.OpenAI
{
    public class ProductAttributeDto
    {
        public string? Brand { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Material { get; set; }
        public string? Type { get; set; }
    }
}
