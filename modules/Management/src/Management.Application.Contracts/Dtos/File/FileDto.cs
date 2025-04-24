using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Management.Dtos.File
{
    public class FileDto : EntityDto<Guid>
    {
        public Guid? TenantId { get; set; }

        public byte[]? DownloadBinary { get; set; }

        public string DownloadObjectId { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public string Filename { get; set; } = string.Empty;

        public string Extension { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public string? Comment { get; set; }
    }

    public class CreateFileDto
    {
        public Guid? TenantId { get; set; }

        public byte[]? DownloadBinary { get; set; }

        public string DownloadObjectId { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public string Filename { get; set; } = string.Empty;

        public string Extension { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }

    public class GetFileListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }
}
