using Microsoft.AspNetCore.Builder;
using Abp.eCommerce;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Abp.eCommerce.Web.csproj"); 
await builder.RunAbpModuleAsync<eCommerceWebTestModule>(applicationName: "Abp.eCommerce.Web");

public partial class Program
{
}
