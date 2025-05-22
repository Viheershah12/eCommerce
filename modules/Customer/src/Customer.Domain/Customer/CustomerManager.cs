using Customer.CustomerGroup;
using Customer.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Customer.Customer
{
    public class CustomerManager : DomainService
    {
        #region Fields
        private readonly ICustomerRepository _customerRepository;
        #endregion

        #region CTOR
        public CustomerManager(
            ICustomerRepository customerRepository    
        ) 
        {
            _customerRepository = customerRepository;
        }
        #endregion 

        public async Task<(List<Models.Customer>, int)> GetListAsync(GetCustomerListingDto dto)
        {
            if (string.IsNullOrEmpty(dto.Sorting))
                dto.Sorting = nameof(Models.Customer.Name);

            var result = await _customerRepository.GetListAsync(dto.Sorting, dto.MaxResultCount, dto.SkipCount, dto.Filter);

            var totalCount = await _customerRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Name.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
