using Abp.eCommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Abp.eCommerce.Web.Public.Pages.Shared.Components.Pagination
{
    public class PaginationViewComponent : AbpViewComponent
    {
        private readonly IConfiguration _appConfiguration;

        public PaginationViewComponent(
            IConfiguration appConfiguration
        )
        {
            _appConfiguration = appConfiguration;
        }

        public async Task<IViewComponentResult> InvokeAsync(dynamic result, string page, string pageHandler, string partialId, bool hasQuickLinks = true, bool hasPageSize = true, object? data = null)
        {
            var pageList = _appConfiguration["PaginationSettings:PageSizeList"];
            var pageListLimit = int.Parse(_appConfiguration["PaginationSettings:PageListLimit"] ?? "5");

            var pageDropdown = new List<SelectListItem>();

            if (pageList != null)
            {
                pageDropdown = pageList.Split(",").Select(x => new SelectListItem
                {
                    Text = x.Trim(),
                    Value = x.Trim()
                }).ToList();
            }

            if (result.PageSize == 0)
                result.PageSize = 10;

            if (result.PageNumber == 0)
                result.PageNumber = 1;

            var model = new PaginationComponentDto
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = (long)Math.Ceiling((double)result.TotalCount / result.PageSize),
                Page = page,
                PageHandler = pageHandler,
                PartialId = partialId,
                HasQuickLinks = hasQuickLinks,
                HasPageSize = hasPageSize,
                PageSizeList = pageDropdown,
                PageListLimit = pageListLimit,
                ResultModel = result,
                Data = data
            };

            return View(model);
        }
    }
}
