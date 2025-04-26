using Customer.Dtos.CustomerGroup;
using Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Customer.Controllers
{
    [Area(CustomerRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CustomerRemoteServiceConsts.RemoteServiceName)]
    [Route("api/customer/customergroup")]
    public class CustomerGroupController : CustomersController, ICustomerGroupAppService
    {
        #region Fields
        private readonly ICustomerGroupAppService _customerGroupAppService;
        #endregion

        #region CTOR
        public CustomerGroupController(ICustomerGroupAppService customerGroupAppService)
        {
            _customerGroupAppService = customerGroupAppService;
        }
        #endregion

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<CustomerGroupDto>> GetListAsync(GetCustomerGroupListDto dto)
        {
            return await _customerGroupAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateCustomerGroupDto dto)
        {
            return await _customerGroupAppService.CreateAsync(dto); 
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateCustomerGroupDto> GetAsync(Guid id)
        {
            return await _customerGroupAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateCustomerGroupDto dto)
        {
            await _customerGroupAppService.UpdateAsync(dto);
        }

        [HttpPut]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _customerGroupAppService.DeleteAsync(id);
        }
    }
}
