using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Product.Models
{
    public class ProductCategory : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }    

        public string? Description { get; set; } 

        public string? Icon { get; set; }

        public Media? CategoryMedia { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }  
    }
}
