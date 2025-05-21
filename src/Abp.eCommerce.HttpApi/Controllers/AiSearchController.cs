using Abp.eCommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Product.Dtos.Product;
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
    [Route("api/eCommerce/aiSearch")]
    public class AiSearchController : eCommerceController, IAiSearchAppService
    {
        #region Fields
        private readonly IAiSearchAppService _aiSearchAppService;
        #endregion

        #region CTOR
        public AiSearchController(
            IAiSearchAppService aiSearchAppService
        )
        {
            _aiSearchAppService = aiSearchAppService;
        }
        #endregion 

        [HttpGet]
        [Route("createCollection")]
        public async Task CreateCollectionAsync()
        {
            await _aiSearchAppService.CreateCollectionAsync();  
        }

        [HttpGet]
        [Route("createPayloadIndex")]
        public async Task CreatePayloadIndexAsync(string fieldName, string fieldSchema)
        {
            await _aiSearchAppService.CreatePayloadIndexAsync(fieldName, fieldSchema);
        }

        [HttpPost]
        [Route("addDataPoints")]
        public async Task AddDataPointAsync(string id, string content, Dictionary<string, object> payload)
        {
            await _aiSearchAppService.AddDataPointAsync(id, content, payload);
        }

        [HttpPost]
        [Route("productSearch")]
        public async Task<List<ProductResultDto>> ProductSearchAsync(ProductSearchDto dto)
        {
            return await _aiSearchAppService.ProductSearchAsync(dto);
        }
    }
}
