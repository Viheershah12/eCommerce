using Abp.eCommerce.Dtos.OpenAI;
using Abp.eCommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.eCommerce.Controllers
{
    [Area(eCommerceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = eCommerceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/eCommerce/openAi")]
    public class OpenAIController : eCommerceController, IOpenAIAppService
    {
        #region Fields
        private readonly IOpenAIAppService _openAIAppService;
        #endregion

        #region CTOR
        public OpenAIController(
            IOpenAIAppService openAIAppService
        ) 
        {
            _openAIAppService = openAIAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getSuggestedProducts")]
        public async Task<string> GetSuggestedProductsAsync(string prompt)
        {
            return await _openAIAppService.GetSuggestedProductsAsync(prompt);   
        }

        [HttpGet]
        [Route("extractProductAttributes")]
        public async Task<ProductAttributeDto> ExtractProductAttributesAsync(string productName)
        {
            return await _openAIAppService.ExtractProductAttributesAsync(productName);
        }
    }
}
