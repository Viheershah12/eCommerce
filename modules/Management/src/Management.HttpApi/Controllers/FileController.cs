using Management.Dtos.File;
using Management.Interfaces;
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
    [Route("api/management/file")]
    public class FileController : ManagementController, IFileAppService
    {
        #region Fields
        private readonly IFileAppService _appService;
        #endregion

        #region CTOR
        public FileController(IFileAppService appService)
        {
            _appService = appService;
        }
        #endregion

        [HttpGet]
        [Route("GetListDownload")]
        public async Task<PagedResultDto<FileDto>> GetListFileAsync(GetFileListDto dto)
        {
            return await _appService.GetListFileAsync(dto);
        }

        [HttpDelete]
        [Route("DeleteFile")]
        public async Task DeleteDownload(Guid id)
        {
            await _appService.DeleteDownload(id);
        }

        [HttpGet]
        [Route("DownloadFilebyId")]
        public async Task<FileDto> DownloadFileByIdAsync(Guid id)
        {
            return await _appService.DownloadFileByIdAsync(id);
        }

        [HttpGet]
        [Route("DownloadMultipleFilebyId")]
        public async Task<List<FileDto>> DownloadMultipleFileByIdAsync(List<Guid> ids)
        {
            return await _appService.DownloadMultipleFileByIdAsync(ids);
        }

        [HttpGet]
        [Route("DownloadFilebyName")]
        public async Task<FileDto> DownloadFileByNameAsync(string filename)
        {
            return await _appService.DownloadFileByNameAsync(filename);
        }

        [HttpPost]
        [Route("InsertFile")]
        public async Task<FileDto> InsertFileAsync(CreateFileDto fileDto)
        {
            return await _appService.InsertFileAsync(fileDto);
        }

        [HttpPost]
        [Route("InsertMultipleFiles")]
        public async Task<List<FileDto>> InsertMultipleFilesAsync(List<CreateFileDto> fileDtos)
        {
            return await _appService.InsertMultipleFilesAsync(fileDtos);
        }
    }
}
