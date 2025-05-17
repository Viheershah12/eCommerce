using System;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abp.eCommerce.Models
{
    #region Base Models
    public class BaseIdModel : EntityDto<Guid>
    {
    }

    public class BaseUserIdModel : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
    }

    public class BaseUserIdTenantModel : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid UserId { get; set; }
    }

    public class BaseIdTenantModel : EntityDto<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
    }

    #endregion

    #region Base Nullable Models
    public class BaseIdNullableModel
    {
        public Guid? Id { get; set; } = Guid.Empty;
    }

    public class BaseTenantNullableModel
    {
        public Guid? TenantId { get; set; }
    }

    public class BaseCompanyNullableModel
    {
        public Guid? CompanyId { get; set; }
    }
    #endregion

    #region Base Audit Models

    public class BaseFullAuditedAggregateRootTenantModel : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
    }

    public class BaseFullAuditedAggregateRootUserModel : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
    }
    #endregion

    #region Pagination
    public class BasePagedModel<T> : PagedResultDto<T>
    {
        public BasePagedModel()
        {
        }

        public BasePagedModel(long totalCount, IReadOnlyList<T> items, int pageSize, int skipCount)
            : base(totalCount, items)
        {
            PageSize = pageSize;
            PageNumber = (skipCount / pageSize) + 1;
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public long TotalPages => (long)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class PaginationComponentDto 
    {
        public PaginationComponentDto()
        {
            PageSizeList = [];
        }

        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public long TotalCount { get; set; }

        public long TotalPages { get; set; }

        public int PageListLimit { get; set; }

        public List<SelectListItem> PageSizeList { get; set; }

        public bool HasQuickLinks { get; set; }

        public bool HasPageSize { get; set; }

        public string Page { get; set; }

        public string PageHandler { get; set; }

        public string PartialId { get; set; }

        public object ResultModel { get; set; }

        public object? Data { get; set; } = null;
    }

    public class BasePaginationModel : PagedResultRequestDto
    {
        private int _pageNumber = 1;
        private int _pageSize = 10;

        /// <summary>
        /// The current page number (1-based).
        /// </summary>
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (value < 1) ? 1 : value;
        }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value < 1) ? 10 : value;
        }

        /// <summary>
        /// The number of items to skip based on the page number and page size.
        /// </summary>
        public override int SkipCount => (PageNumber - 1) * PageSize;

        /// <summary>
        /// The maximum number of items to retrieve.
        /// </summary>
        public override int MaxResultCount => PageSize;
    }
    #endregion


    public class Media : BaseIdModel
    {
        public string Name { get; set; }
    }

    public class UserFileDto : BaseIdModel
    {
        public byte[]? DownloadBinary { get; set; }

        public string DownloadObjectId { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public string Filename { get; set; } = string.Empty;

        public string Extension { get; set; } = string.Empty;
    }
}
