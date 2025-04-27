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
        //public Guid Id { get; set; }
    }

    public class BaseCompanyModel
    {
        public Guid CompanyId { get; set; }
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

    public class BaseCompanyTenantModel : BaseCompanyModel, IMultiTenant
    {
        public Guid? TenantId { get; set; }
    }

    public class BaseIdCompanyTenantModel : BaseIdTenantModel
    {
        public Guid CompanyId { get; set; }
    }

    public class BaseIdCompanyModel : EntityDto<Guid>
    {
        public Guid CompanyId { get; set; }
    }
    public class BaseNullableIdCompanyModel : EntityDto<Guid?>
    {
        public Guid CompanyId { get; set; }
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
    public class BaseCompanyFullAuditedAggregateRootModel : FullAuditedAggregateRoot<Guid>
    {
        public Guid CompanyId { get; set; }
    }

    public class BaseCompanyFullAuditedAggregateRootTenantModel : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid CompanyId { get; set; }

        public Guid? TenantId { get; set; }
    }

    public class BaseFullAuditedAggregateRootTenantModel : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
    }
    #endregion

    #region Pagination
    public class BasePaginationModel<T> : PagedResultDto<T>
    {
        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public long TotalPages { get; set; } = 0;
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
