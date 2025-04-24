using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Management.Dtos.Content
{
    public class GetContentListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }

    public class ContentDto : BaseIdModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsPublished { get; set; }

        public string ContentType { get; set; }
    }

    public class CreateUpdateContentDto : BaseIdModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsPublished { get; set; }

        public ContentType ContentType { get; set; }
    }
}
