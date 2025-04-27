using Customer.Dtos.CustomerGroup;
using Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Customer.Controllers
{
    [Area(CustomerRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CustomerRemoteServiceConsts.RemoteServiceName)]
    [Route("api/customer/common")]
    public class CommonController : CustomersController, ICommonAppService
    {
        #region Fields
        private readonly ICommonAppService _commonAppService;
        #endregion

        #region CTOR
        public CommonController(ICommonAppService commonAppService) 
        {
            _commonAppService = commonAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getCustomerGroupById")]
        public async Task<List<CustomerGroupDto>> GetCutomerGroupByIdAsync(List<Guid> ids)
        {
            return await _commonAppService.GetCutomerGroupByIdAsync(ids);
        }

        [HttpGet]
        [Route("getCustomerGroupDropdown")]
        public async Task<List<CustomerGroupDto>> GetCustomerGroupDropdownAsync()
        {
            return await _commonAppService.GetCustomerGroupDropdownAsync();
        }
    }
}
