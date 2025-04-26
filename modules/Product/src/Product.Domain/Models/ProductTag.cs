using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Product.Models
{
    public class ProductTag : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
