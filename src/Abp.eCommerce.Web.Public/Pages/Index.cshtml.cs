using Abp.eCommerce.Web.Public.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Abp.eCommerce.Web.Pages;

public class IndexModel : eCommerceWebPublicPageModel
{
    protected IdentityUserManager UserManager { get; }

    private readonly IIdentityUserRepository _userRepository;

    public string? PasswordlessLoginUrl { get; set; }

    public string Email { get; set; }

    public IndexModel(IdentityUserManager userManager, IIdentityUserRepository userRepository)
    {
        UserManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            var adminUser = await _userRepository.FindByNormalizedUserNameAsync("GUEST");

            var token = await UserManager.GenerateUserTokenAsync(adminUser, "PasswordlessLoginProvider",
                "passwordless-auth");

            PasswordlessLoginUrl = Url.Action("Login", "Passwordless", new { token, userId = adminUser.Id.ToString() }, Request.Scheme);
        }

        return Page();
    }
}
