using Abp.eCommerce.Models;
using Customer.CustomerGroup;
using Customer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Customer.Dtos.CustomerGroup;

namespace Customer.Services
{
    public class CustomerGroupAppService : CustomersAppService, ICustomerGroupAppService
    {
        #region Fields
        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly CustomerGroupManager _customerGroupManager;
        #endregion

        #region CTOR
        public CustomerGroupAppService(
            ICustomerGroupRepository customerGroupRepository,
            CustomerGroupManager customerGroupManager
        ) 
        {
            _customerGroupRepository = customerGroupRepository;
            _customerGroupManager = customerGroupManager;
        }
        #endregion

        public async Task<PagedResultDto<CustomerGroupDto>> GetListAsync(GetCustomerGroupListDto dto)
        {
            try
            {
                var (items, totalCount) = await _customerGroupManager.GetProductListing(dto);
                var list = ObjectMapper.Map<List<Models.CustomerGroup>, List<CustomerGroupDto>>(items);

                return new PagedResultDto<CustomerGroupDto>(totalCount, list);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateCustomerGroupDto dto)
        {
            try
            {
                var productCategoryDto = ObjectMapper.Map<CreateUpdateCustomerGroupDto, Models.CustomerGroup>(dto);
                var productCategory = await _customerGroupRepository.InsertAsync(productCategoryDto);

                return productCategory.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateCustomerGroupDto> GetAsync(Guid id)
        {
            try
            {
                var productCategory = await _customerGroupRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.CustomerGroup, CreateUpdateCustomerGroupDto>(productCategory);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateCustomerGroupDto dto)
        {
            try
            {
                var productCategory = await _customerGroupRepository.GetAsync(dto.Id);

                var updatedProductCategory = ObjectMapper.Map<CreateUpdateCustomerGroupDto, Models.CustomerGroup>(dto);
                updatedProductCategory.ConcurrencyStamp = productCategory.ConcurrencyStamp;

                await _customerGroupRepository.UpdateAsync(updatedProductCategory, true);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var productCategory = await _customerGroupRepository.GetAsync(id);
                await _customerGroupRepository.DeleteAsync(productCategory);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
