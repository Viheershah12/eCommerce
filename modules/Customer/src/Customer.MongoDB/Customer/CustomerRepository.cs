using Customer.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Customer.Customer
{
    public class CustomerRepository : MongoDbRepository<CustomerMongoDbContext, Models.Customer, Guid>, ICustomerRepository 
    {
        public CustomerRepository(IMongoDbContextProvider<CustomerMongoDbContext> contextProvider) : base(contextProvider) { }

        public virtual async Task<List<Models.Customer>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetFilteredQueryableAsync(
                filter,
                roleId,
                organizationUnitId,
                userName,
                phoneNumber,
                emailAddress,
                name,
                surname,
                isLockedOut,
                notActive,
                emailConfirmed,
                isExternal,
                maxCreationTime,
                minCreationTime,
                maxModifitionTime,
                minModifitionTime,
                cancellationToken
            );

            return await query
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(Models.Customer.CreationTime) + " desc" : sorting)
                .As<IMongoQueryable<Models.Customer>>()
                .PageBy<Models.Customer, IMongoQueryable<Models.Customer>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<Models.Customer> GetAsync(Guid id)
        {
            var query = await GetMongoQueryableAsync();

            return await query
                .Where(x => x.Id == id)
                .As<IMongoQueryable<Models.Customer>>()
                .FirstOrDefaultAsync();
        }


        protected virtual async Task<IMongoQueryable<Models.Customer>> GetFilteredQueryableAsync(
        string? filter = null,
        Guid? roleId = null,
        Guid? organizationUnitId = null,
        string? userName = null,
        string? phoneNumber = null,
        string? emailAddress = null,
        string? name = null,
        string? surname = null,
        bool? isLockedOut = null,
        bool? notActive = null,
        bool? emailConfirmed = null,
        bool? isExternal = null,
        DateTime? maxCreationTime = null,
        DateTime? minCreationTime = null,
        DateTime? maxModifitionTime = null,
        DateTime? minModifitionTime = null,
        CancellationToken cancellationToken = default)
        {
            //#ana do we have an issue with tenancy here?
            var upperFilter = filter?.ToUpperInvariant();
            var query = await GetMongoQueryableAsync(cancellationToken);

            if (roleId.HasValue)
            {
                //var organizationUnitIds = (await GetMongoQueryableAsync<OrganizationUnit>(cancellationToken))
                //    .Where(ou => ou.Roles.Any(r => r.RoleId == roleId.Value))
                //    .Select(userOrganizationUnit => userOrganizationUnit.Id)
                //    .ToArray();

                //query = query.Where(identityUser => identityUser.Roles.Any(x => x.RoleId == roleId.Value) || identityUser.OrganizationUnits.Any(x => organizationUnitIds.Contains(x.OrganizationUnitId)));
                query = query.Where(identityUser => identityUser.Roles.Any(x => x.RoleId == roleId.Value));
            }

            return query
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(
                    !filter.IsNullOrWhiteSpace() && !upperFilter.IsNullOrEmpty(),
                    u =>
                        u.NormalizedUserName.Contains(upperFilter) ||
                        u.NormalizedEmail.Contains(upperFilter) ||
                        (u.Name != null && u.Name.Contains(filter)) ||
                        (u.Surname != null && u.Surname.Contains(filter)) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                )
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(organizationUnitId.HasValue, identityUser => identityUser.OrganizationUnits.Any(x => x.OrganizationUnitId == organizationUnitId.Value))
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(!string.IsNullOrWhiteSpace(userName), x => x.UserName == userName)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(!string.IsNullOrWhiteSpace(phoneNumber), x => x.PhoneNumber == phoneNumber)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(!string.IsNullOrWhiteSpace(emailAddress), x => x.Email == emailAddress)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(!string.IsNullOrWhiteSpace(name), x => x.Name == name)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(!string.IsNullOrWhiteSpace(surname), x => x.Surname == surname)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(isLockedOut.HasValue && isLockedOut.Value, x => x.LockoutEnabled && x.LockoutEnd != null && x.LockoutEnd > DateTimeOffset.UtcNow)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(isLockedOut.HasValue && !isLockedOut.Value, x => !(x.LockoutEnabled && x.LockoutEnd != null && x.LockoutEnd > DateTimeOffset.UtcNow))
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(notActive.HasValue, x => x.IsActive == !notActive.Value)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(emailConfirmed.HasValue, x => x.EmailConfirmed == emailConfirmed.Value)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(isExternal.HasValue, x => x.IsExternal == isExternal.Value)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(maxCreationTime != null, p => p.CreationTime <= maxCreationTime)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(minCreationTime != null, p => p.CreationTime >= minCreationTime)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(maxModifitionTime != null, p => p.LastModificationTime <= maxModifitionTime)
                .WhereIf<Models.Customer, IMongoQueryable<Models.Customer>>(minModifitionTime != null, p => p.LastModificationTime >= minModifitionTime);
        }
    }
}
