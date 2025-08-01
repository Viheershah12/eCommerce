﻿
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Abp.eCommerce.Web.Public.PasswordlessAuthentication
{
    public class PasswordlessLoginProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser>
            where TUser : class
    {
        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            return Task.FromResult(false);
        }

        //We need to override this method as well.
        public async override Task<string> GetUserModifierAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            var userId = await manager.GetUserIdAsync(user);

            return "PasswordlessLogin:" + purpose + ":" + userId;
        }
    }
}
