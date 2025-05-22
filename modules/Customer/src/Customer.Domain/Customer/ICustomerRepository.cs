using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Customer.Customer
{
    public interface ICustomerRepository : IRepository<Models.Customer, Guid>
    {
        Task<List<Models.Customer>> GetListAsync(
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string? filter = null,
            bool includeDetails = false,
            Guid? roleId = null,
            Guid? organizationUnitId = null,
            string userName = null,
            string phoneNumber = null,
            string emailAddress = null,
            string name = null,
            string surname = null,
            bool? isLockedOut = null,
            bool? notActive = null,
            bool? emailConfirmed = null,
            bool? isExternal = null,
            DateTime? maxCreationTime = null,
            DateTime? minCreationTime = null,
            DateTime? maxModifitionTime = null,
            DateTime? minModifitionTime = null,
            CancellationToken cancellationToken = default);
    }
}
