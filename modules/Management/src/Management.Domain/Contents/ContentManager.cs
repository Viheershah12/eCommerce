using Management.Dtos.Content;
using Management.Files;
using Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Management.Contents
{
    public class ContentManager : DomainService
    {
        #region Fields
        private readonly IContentRepository _contentRepository;
        #endregion

        #region CTOR
        public ContentManager(
            IContentRepository contentRepository            
        )
        {
            _contentRepository = contentRepository;
        }
        #endregion

        public async Task<(List<Content> items, int totalCount)> GetContentListing(GetContentListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Content.Title);

            var result = await _contentRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _contentRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Title.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
