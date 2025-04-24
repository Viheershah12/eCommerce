using Management.Dtos.File;
using Management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Management.Files
{
    public class FileManager : DomainService
    {
        #region Fields 
        private readonly IFileRepository _fileRepository;
        #endregion

        #region CTOR
        public FileManager(
            IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        #endregion

        public async Task<(List<File> items, int totalCount)> GetDownloadFileListing(GetFileListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(File.Filename);

            var result = await _fileRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _fileRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Filename.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
