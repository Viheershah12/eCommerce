using Management.Files;
using Management.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Management.Models;
using Management.Dtos.File;

namespace Management.Services
{
    public class FileAppService : ManagementAppService, IFileAppService
    {
        #region Fields 
        private readonly IFileRepository _fileRepository;
        private readonly FileManager _fileManager;
        #endregion

        #region CTOR
        public FileAppService(
            IFileRepository fileRepository,
            FileManager fileManager
        )
        {
            _fileRepository = fileRepository;
            _fileManager = fileManager;
        }
        #endregion

        public async Task<PagedResultDto<FileDto>> GetListFileAsync(GetFileListDto dto)
        {
            try
            {
                var (items, totalCount) = await _fileManager.GetDownloadFileListing(dto);

                var listing = items.Select(x => new FileDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Filename = x.Filename,
                    DownloadBinary = x.DownloadBinary,
                    DownloadObjectId = x.DownloadObjectId,
                    ContentType = x.ContentType,
                    Extension = x.Extension
                }).ToList();

                return new PagedResultDto<FileDto>(totalCount, listing);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<FileDto> InsertFileAsync(CreateFileDto fileDto)
        {
            try
            {
                if (fileDto == null)
                    throw new BusinessException("File is empty");

                if (fileDto.DownloadBinary == null)
                    throw new BusinessException("Download Binary is empty");

                var file = new File
                {
                    DownloadBinary = fileDto.DownloadBinary,
                    Filename = fileDto.Filename,
                    UserId = fileDto.UserId,
                    ContentType = fileDto.ContentType,
                    Extension = fileDto.Extension
                };

                var dbFile = await _fileRepository.UploadFileAsync(file);

                return ObjectMapper.Map<File, FileDto>(dbFile);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<List<FileDto>> InsertMultipleFilesAsync(List<CreateFileDto> fileDtos)
        {
            try
            {
                if (fileDtos == null || !fileDtos.Any())
                    throw new BusinessException("Files Are Empty");

                var uploadedFiles = new List<FileDto>();

                foreach (var fileDto in fileDtos)
                {
                    if (fileDto.DownloadBinary == null)
                        throw new BusinessException($"Download Binary is empty for file: {fileDto.Filename}");

                    var file = new File
                    {
                        DownloadBinary = fileDto.DownloadBinary,
                        Filename = fileDto.Filename,
                        UserId = fileDto.UserId,
                        ContentType = fileDto.ContentType,
                        Extension = fileDto.Extension
                    };

                    var dbFile = await _fileRepository.UploadFileAsync(file);
                    uploadedFiles.Add(ObjectMapper.Map<File, FileDto>(dbFile));
                }

                return uploadedFiles;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<FileDto> DownloadFileByIdAsync(Guid id)
        {
            try
            {
                var file = await _fileRepository.GetAsync(id);

                if (file == null)
                    throw new BusinessException("File does not exists");

                if (string.IsNullOrEmpty(file?.DownloadObjectId))
                    throw new BusinessException("File download object id empty");

                file.DownloadBinary = await _fileRepository.GetFileByIdAsync(file.DownloadObjectId);

                return ObjectMapper.Map<File, FileDto>(file);

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<List<FileDto>> DownloadMultipleFileByIdAsync(List<Guid> ids)
        {
            try
            {
                var res = new List<FileDto>();

                foreach (var id in ids)
                {
                    var file = await _fileRepository.GetAsync(id);

                    if (file == null)
                        throw new BusinessException("File does not exists");

                    if (string.IsNullOrEmpty(file?.DownloadObjectId))
                        throw new BusinessException("File download object id empty");

                    file.DownloadBinary = await _fileRepository.GetFileByIdAsync(file.DownloadObjectId);

                    res.Add(ObjectMapper.Map<File, FileDto>(file));
                }

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<FileDto> DownloadFileByNameAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new BusinessException("The filename is empty");

            try
            {
                var file = await _fileRepository.FindAsync(x => x.Filename == filename);

                if (file == null)
                    throw new BusinessException("File does not exists");

                file.DownloadBinary = await _fileRepository.GetFileByNameAsync(filename);

                return ObjectMapper.Map<File, FileDto>(file);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task DeleteDownload(Guid id)
        {
            try
            {
                var file = await _fileRepository.GetAsync(id);

                if (file == null)
                    throw new BusinessException("File does not exists");

                if (string.IsNullOrEmpty(file?.DownloadObjectId))
                    throw new BusinessException("File download object id does not exits");

                await _fileRepository.DeleteFileAsync(file);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
