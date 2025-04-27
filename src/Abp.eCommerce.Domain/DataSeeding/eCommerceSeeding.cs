using Management.Files;
using Microsoft.AspNetCore.Identity;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Reflection;

namespace Abp.eCommerce.DataSeeding
{
    internal class eCommerceSeeding : IDataSeedContributor, ITransientDependency
    {
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        public eCommerceSeeding(
            IIdentityRoleRepository roleRepository, 
            RoleManager<IdentityRole> roleManager,
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionDataSeeder permissionDataSeeder
        )
        {
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _permissionDefinitionManager = permissionDefinitionManager;
            _permissionDataSeeder = permissionDataSeeder;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await CreateRoleIfNotExistsAsync("superadmin", true);

            await CreateRoleIfNotExistsAsync("customer");

            await CreateRoleIfNotExistsAsync("manager");
        }

        private async Task CreateRoleIfNotExistsAsync(string roleName, bool grantAllPermissions = false, string[]? permissions = null)
        {
            var existingRole = await _roleRepository.FindByNormalizedNameAsync(roleName.ToUpperInvariant());
            if (existingRole == null)
            {
                var role = new IdentityRole(Guid.NewGuid(), roleName)
                {
                    IsStatic = true,
                    IsPublic = true,
                };

                var roleResult = await _roleManager.CreateAsync(role);

                if (roleResult.Succeeded)
                {
                    if (grantAllPermissions)
                    {
                        var permissionNames = (await _permissionDefinitionManager.GetPermissionsAsync())
                            .Where(p => !p.Providers.Any() || p.Providers.Contains(RolePermissionValueProvider.ProviderName))
                            .Select(p => p.Name)
                            .ToArray();

                        await _permissionDataSeeder.SeedAsync(
                            RolePermissionValueProvider.ProviderName,
                            roleName,
                            permissionNames
                        );
                    }
                    else if (permissions != null)
                    {
                        await _permissionDataSeeder.SeedAsync(
                            RolePermissionValueProvider.ProviderName,
                            roleName,
                            permissions
                        );
                    }
                }
            }
        }

    }
}
