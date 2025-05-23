using Customer.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Customer.Interfaces
{
    public interface ICustomerAppService : IApplicationService
    {
        #region CRUD
        Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListingDto dto);

        Task<Guid> CreateAsync(CreateUpdateCustomerDto dto);

        Task<CustomerDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateCustomerDto dto);

        Task DeleteAsync(Guid id);
        #endregion

        #region Address
        Task UpdateBillingAddressAsync(AddressDto dto);

        Task UpdateShippingAddressAsync(AddressDto dto);
        #endregion 
    }
}
