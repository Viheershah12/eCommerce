using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Management.Models
{
    public class File : FullAuditedAggregateRoot<Guid>
    {
        public byte[]? DownloadBinary { get; set; }

        public string DownloadObjectId { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public string Filename { get; set; } = string.Empty;

        public string Extension { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
