using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using Customer.Customer;
using Customer.Dtos.Customer;
using Customer.Interfaces;
using Customer.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace Customer.Services
{
    public class CustomerAppService : CustomersAppService, ICustomerAppService
    {
        #region Fields
        private readonly CustomerManager _customerManager;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IdentityUserManager _identityUserManager;
        #endregion

        #region CTOR
        public CustomerAppService(
            CustomerManager customerManager,
            IIdentityUserAppService identityUserAppService,
            IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            ICustomerRepository customerRepository,
            IdentityUserManager identityUserManager
        )
        {
            _customerManager = customerManager;
            _identityUserAppService = identityUserAppService;
            _identityUserRepository = identityUserRepository;
            _identityRoleRepository = identityRoleRepository;
            _customerRepository = customerRepository;
            _identityUserManager = identityUserManager;
        } 
        #endregion 

        public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListingDto dto)
        {
            try
            {
                var (items, totalCount) = await _customerManager.GetListAsync(dto);
                var list = ObjectMapper.Map<List<Models.Customer>, List<CustomerDto>>(items);

                return new PagedResultDto<CustomerDto>(totalCount, list);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateCustomerDto dto)
        {
            try
            {
                var userDto = ObjectMapper.Map<CreateUpdateCustomerDto, IdentityUserCreateDto>(dto);
                userDto.RoleNames = ["Customer"];

                // Set extra properties using extension manager
                userDto.SetProperty("Gender", dto.Gender);
                userDto.SetProperty("GenderName", dto.GenderName);
                userDto.SetProperty("DateOfBirth", dto.DateOfBirth);
                userDto.SetProperty("HomePhoneNumber", dto.HomePhoneNumber);
                userDto.SetProperty("IdentificationType", dto.IdentificationType);
                userDto.SetProperty("IdentificationNo", dto.IdentificationNo);
                userDto.SetProperty("BillingAddress", dto.BillingAddress);
                userDto.SetProperty("ShippingAddress", dto.ShippingAddress);

                var user = await _identityUserAppService.CreateAsync(userDto);
                return user.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            try
            {
                var customer = await _identityUserManager.GetByIdAsync(id);
                var res = ObjectMapper.Map<IdentityUser, CustomerDto>(customer);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        
        public async Task UpdateAsync(CreateUpdateCustomerDto dto)
        {
            try
            {
                var customer = await _customerRepository.GetAsync(dto.Id);

                var userDto = ObjectMapper.Map<CreateUpdateCustomerDto, IdentityUserUpdateDto>(dto);
                userDto.ConcurrencyStamp = customer.ConcurrencyStamp;
                userDto.RoleNames = ["Customer"];

                // Set extra properties using extension manager
                userDto.SetProperty("Gender", dto.Gender);
                userDto.SetProperty("GenderName", dto.GenderName);
                userDto.SetProperty("DateOfBirth", dto.DateOfBirth);
                userDto.SetProperty("HomePhoneNumber", dto.HomePhoneNumber);
                userDto.SetProperty("IdentificationType", dto.IdentificationType);
                userDto.SetProperty("IdentificationNo", dto.IdentificationNo);
                userDto.SetProperty("BillingAddress", dto.BillingAddress);
                userDto.SetProperty("ShippingAddress", dto.ShippingAddress);

                await _identityUserAppService.UpdateAsync(dto.Id, userDto);
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
                var customer = await _customerRepository.GetAsync(id);
                await _customerRepository.DeleteAsync(customer);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
