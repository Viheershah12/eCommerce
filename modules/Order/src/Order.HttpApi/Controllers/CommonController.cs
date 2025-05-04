using Microsoft.AspNetCore.Mvc;
using Order.Dtos.Common;
using Order.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Order.Controllers
{
    [Area(OrderRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = OrderRemoteServiceConsts.RemoteServiceName)]
    [Route("api/order/common")]
    public class CommonController : OrderController, ICommonAppService
    {
        private readonly ICommonAppService _commonAppService;

        public CommonController(ICommonAppService commonAppService)
        {
            _commonAppService = commonAppService;
        }

        [HttpGet]
        [Route("getShoppingCartCount")]
        public async Task<StatisticDto> GetStatisticsAsync(Guid userId)
        {
            return await _commonAppService.GetStatisticsAsync(userId);
        }
    }
}
