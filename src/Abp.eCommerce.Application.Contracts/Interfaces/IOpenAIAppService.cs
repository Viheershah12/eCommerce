using Abp.eCommerce.Dtos.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.eCommerce.Interfaces
{
    public interface IOpenAIAppService : IApplicationService
    {
        Task<string> GetSuggestedProductsAsync(string prompt);

        Task<ProductAttributeDto> ExtractProductAttributesAsync(string productName);
    }
}
