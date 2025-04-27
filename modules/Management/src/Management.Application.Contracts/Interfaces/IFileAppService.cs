using Management.Dtos.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Management.Interfaces
{
    public interface IFileAppService : IApplicationService
    {
        Task<PagedResultDto<FileDto>> GetListFileAsync(GetFileListDto dto);

        Task<FileDto> InsertFileAsync(CreateFileDto fileDto);

        Task<List<FileDto>> InsertMultipleFilesAsync(List<CreateFileDto> fileDtos);

        Task<FileDto> DownloadFileByIdAsync(Guid id);

        Task<List<FileDto>> DownloadMultipleFileByIdAsync(List<Guid> ids);

        Task<FileDto> DownloadFileByNameAsync(string fileName);

        Task DeleteDownload(Guid id);

        Task DeleteDownloadMany(List<Guid> ids);
    }
}
