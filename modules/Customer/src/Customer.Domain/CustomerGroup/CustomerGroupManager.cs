using Customer.Dtos.CustomerGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Customer.CustomerGroup
{
    public class CustomerGroupManager : DomainService
    {
        #region Fields
        private readonly ICustomerGroupRepository _customerGroupRepository;
        #endregion

        #region CTOR
        public CustomerGroupManager(ICustomerGroupRepository customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }
        #endregion

        public async Task<(List<Models.CustomerGroup> items, int totalCount)> GetProductListing(GetCustomerGroupListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.CustomerGroup.Name);

            var result = await _customerGroupRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _customerGroupRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Name.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
