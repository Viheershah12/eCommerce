using System.Threading.Tasks;

namespace Abp.eCommerce.Web.Public.PageManagement;

public interface IProfileManagementPageContributor
{
    Task ConfigureAsync(ProfileManagementPageCreationContext context);
}
