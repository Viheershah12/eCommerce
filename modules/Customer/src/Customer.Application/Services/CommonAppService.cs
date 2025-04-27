using Customer.CustomerGroup;
using Customer.Dtos.CustomerGroup;
using Customer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Customer.Services
{
    public class CommonAppService : CustomersAppService, ICommonAppService
    {
        #region Fields
        private readonly ICustomerGroupRepository _customerGroupRepository;
        #endregion

        #region CTOR
        public CommonAppService(
            ICustomerGroupRepository customerGroupRepository
        )
        {
            _customerGroupRepository = customerGroupRepository;
        }
        #endregion 

        public async Task<List<CustomerGroupDto>> GetCutomerGroupByIdAsync(List<Guid> ids)
        {
            try
            {
                var groups = await _customerGroupRepository.GetListAsync(x => ids.Contains(x.Id));
                var res = ObjectMapper.Map<List<Models.CustomerGroup>, List<CustomerGroupDto>>(groups);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<List<CustomerGroupDto>> GetCustomerGroupDropdownAsync()
        {
            try
            {
                var groups = await _customerGroupRepository.GetListAsync(x => x.IsActive);
                var res = ObjectMapper.Map<List<Models.CustomerGroup>, List<CustomerGroupDto>>(groups);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);    
            }
        }
    }
}
