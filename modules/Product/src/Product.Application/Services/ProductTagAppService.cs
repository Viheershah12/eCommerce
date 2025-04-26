using Abp.eCommerce.Models;
using Management.Dtos.File;
using Product.Dtos.Common;
using Product.Dtos.ProductCategory;
using Product.Interfaces;
using Product.ProductCategory;
using Product.ProductTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Product.Dtos.ProductTag;

namespace Product.Services
{
    public class ProductTagAppService : ProductsAppService, IProductTagAppService
    {
        #region Fields
        private readonly IProductTagRepository _productTagRepository;
        private readonly ProductTagManager _productTagManager;
        #endregion

        #region CTOR
        public ProductTagAppService(
            IProductTagRepository productTagRepository,
            ProductTagManager productTagManager
        ) 
        {
            _productTagRepository = productTagRepository;
            _productTagManager = productTagManager;
        }
        #endregion

        public async Task<PagedResultDto<ProductTagDto>> GetListAsync(GetProductTagListDto dto)
        {
            try
            {
                var (items, totalCount) = await _productTagManager.GetProductListing(dto);
                var list = ObjectMapper.Map<List<Models.ProductTag>, List<ProductTagDto>>(items);

                return new PagedResultDto<ProductTagDto>(totalCount, list);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateProductTagDto dto)
        {
            try
            {
                var productTagDto = ObjectMapper.Map<CreateUpdateProductTagDto, Models.ProductTag>(dto);

                var productTag = await _productTagRepository.InsertAsync(productTagDto);
                return productTag.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateProductTagDto> GetAsync(Guid id)
        {
            try
            {
                var productTag = await _productTagRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.ProductTag, CreateUpdateProductTagDto>(productTag);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateProductTagDto dto)
        {
            try
            {
                var productTag = await _productTagRepository.GetAsync(dto.Id);

                var updatedProductTag = ObjectMapper.Map<CreateUpdateProductTagDto, Models.ProductTag>(dto);
                updatedProductTag.ConcurrencyStamp = productTag.ConcurrencyStamp;

                await _productTagRepository.UpdateAsync(updatedProductTag, true);
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
                var productTag = await _productTagRepository.GetAsync(id);
                await _productTagRepository.DeleteAsync(productTag);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
