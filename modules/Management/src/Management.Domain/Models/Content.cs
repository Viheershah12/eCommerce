using Abp.eCommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Management.Models
{
    public class Content : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsPublished { get; set; }

        public ContentType ContentType { get; set; }
    }
}
