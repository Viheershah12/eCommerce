using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace PaymentTransactions.Samples;

[Area(PaymentTransactionsRemoteServiceConsts.ModuleName)]
[RemoteService(Name = PaymentTransactionsRemoteServiceConsts.RemoteServiceName)]
[Route("api/PaymentTransactions/sample")]
public class SampleController : PaymentTransactionsController, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public SampleController(ISampleAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}
