using Order.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Order.Interfaces
{
    public interface ICommonAppService : IApplicationService
    {
        Task<StatisticDto> GetStatisticsAsync(Guid userId);
    }
}
