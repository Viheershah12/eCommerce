using Abp.eCommerce.Models;
using Product.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dtos.ProductCategory
{
    public class CreateUpdateProductCategoryDto : BaseIdModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public UserFileDto? CategoryMedia { get; set; }

        public MediaDto? UploadedMedia { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
