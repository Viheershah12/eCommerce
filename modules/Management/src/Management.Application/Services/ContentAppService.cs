using Management.Contents;
using Management.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Management.Models;
using Management.Dtos.Content;

namespace Management.Services
{
    public class ContentAppService : ManagementAppService, IContentAppService
    {
        #region Fields
        private readonly IContentRepository _contentRepository;
        private readonly ContentManager _contentManager;
        #endregion

        #region CTOR
        public ContentAppService(
            IContentRepository contentRepository,
            ContentManager contentManager
        ) 
        {
            _contentRepository = contentRepository;
            _contentManager = contentManager;
        }
        #endregion

        public async Task<PagedResultDto<ContentDto>> GetListAsync(GetContentListDto dto)
        {
            try
            {
                var (items, totalCount) = await _contentManager.GetContentListing(dto);
                var list = ObjectMapper.Map<List<Content>, List<ContentDto>>(items);

                return new PagedResultDto<ContentDto>(totalCount, list);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateContentDto dto)
        {
            try
            {
                var contentDto = ObjectMapper.Map<CreateUpdateContentDto, Content>(dto);
                var content = await _contentRepository.InsertAsync(contentDto);

                return content.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateContentDto> GetAsync(Guid id)
        {
            try
            {
                var content = await _contentRepository.GetAsync(id);
                var res = ObjectMapper.Map<Content, CreateUpdateContentDto>(content);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateContentDto dto)
        {
            try
            {
                var content = await _contentRepository.GetAsync(dto.Id);

                var updatedContent = ObjectMapper.Map<CreateUpdateContentDto, Content>(dto);
                updatedContent.ConcurrencyStamp = content.ConcurrencyStamp;

                await _contentRepository.UpdateAsync(updatedContent, true);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var content = await _contentRepository.GetAsync(id);
                await _contentRepository.DeleteAsync(content);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
