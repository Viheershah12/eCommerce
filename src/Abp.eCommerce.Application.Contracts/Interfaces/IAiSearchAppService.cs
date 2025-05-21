using Product.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.eCommerce.Interfaces
{
    public interface IAiSearchAppService : IApplicationService
    {
        Task CreateCollectionAsync();

        Task CreatePayloadIndexAsync(string fieldName, string fieldSchema);

        Task AddDataPointAsync(string id, string content, Dictionary<string, object> payload);

        Task<List<ProductResultDto>> ProductSearchAsync(ProductSearchDto dto);
    }
}
