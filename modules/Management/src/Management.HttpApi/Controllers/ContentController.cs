using Management.Dtos.Content;
using Management.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Management.Controllers
{
    [Area(ManagementRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = ManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("api/management/content")]
    public class ContentController : ManagementController, IContentAppService
    {
        #region Fields
        private readonly IContentAppService _contentAppService;
        #endregion

        #region CTOR
        public ContentController(
            IContentAppService contentAppService            
        )
        {
            _contentAppService = contentAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<ContentDto>> GetListAsync(GetContentListDto dto)
        {
            return await _contentAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateContentDto dto)
        {
            return await _contentAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateContentDto> GetAsync(Guid id)
        {
            return await _contentAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateContentDto dto)
        {
            await _contentAppService.UpdateAsync(dto);
        }

        [HttpPut]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _contentAppService.DeleteAsync(id);
        }
    }
}
