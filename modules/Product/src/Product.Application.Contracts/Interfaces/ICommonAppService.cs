using Product.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Product.Interfaces
{
    public interface ICommonAppService : IApplicationService
    {
        Task<List<ProductCategoryDropdownDto>> GetProductCategoryDropdownAsync();
    }
}
