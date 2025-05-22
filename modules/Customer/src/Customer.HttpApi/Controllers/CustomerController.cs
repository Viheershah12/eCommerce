using Customer.Dtos.Customer;
using Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/customers/customer")]
    public class CustomerController : CustomersController, ICustomerAppService
    {
        #region Fields
        private readonly ICustomerAppService _customerAppService;
        #endregion

        #region CTOR
        public CustomerController(ICustomerAppService customerAppService) 
        {
            _customerAppService = customerAppService;
        }
        #endregion

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListingDto dto)
        {
            return await _customerAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateCustomerDto dto)
        {
            return await _customerAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CustomerDto> GetAsync(Guid id)
        {
            return await _customerAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateCustomerDto dto)
        {
            await _customerAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _customerAppService.DeleteAsync(id);
        }
    }
}
