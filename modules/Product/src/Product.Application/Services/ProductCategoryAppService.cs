using Abp.eCommerce.Models;
using Management.Dtos.File;
using Management.Interfaces;
using Product.Dtos.Common;
using Product.Dtos.ProductCategory;
using Product.Interfaces;
using Product.Models;
using Product.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Product.Services
{
    public class ProductCategoryAppService : ProductsAppService, IProductCategoryAppService
    {
        #region Fields
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ProductCategoryManager _productCategoryManager;
        private readonly IFileAppService _fileAppService;
        #endregion

        #region CTOR
        public ProductCategoryAppService(
            IProductCategoryRepository productCategoryRepository,
            ProductCategoryManager productCategoryManager,
            IFileAppService fileAppService
        ) 
        {
            _productCategoryRepository = productCategoryRepository;
            _productCategoryManager = productCategoryManager;
            _fileAppService = fileAppService;
        }
        #endregion 

        public async Task<PagedResultDto<ProductCategoryDto>> GetListAsync(GetProductCategoryListDto dto)
        {
            try
            {
                var (items, totalCount) = await _productCategoryManager.GetProductListing(dto);
                var list = ObjectMapper.Map<List<Models.ProductCategory>, List<ProductCategoryDto>>(items);

                return new PagedResultDto<ProductCategoryDto>(totalCount, list);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateProductCategoryDto dto)
        {
            try
            {
                var productCategoryDto = ObjectMapper.Map<CreateUpdateProductCategoryDto, Models.ProductCategory>(dto);

                // Add To File Table
                if (dto.CategoryMedia != null)
                {
                    var file = ObjectMapper.Map<UserFileDto, CreateFileDto>(dto.CategoryMedia);
                    var uploadedFile = await _fileAppService.InsertFileAsync(file);

                    productCategoryDto.CategoryMedia = new Media
                    {
                        Id = uploadedFile.Id,
                        Name = uploadedFile.Filename
                    };
                }

                var productCategory = await _productCategoryRepository.InsertAsync(productCategoryDto);
                return productCategory.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateProductCategoryDto> GetAsync(Guid id)
        {
            try
            {
                var productCategory = await _productCategoryRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.ProductCategory, CreateUpdateProductCategoryDto>(productCategory);

                // Get File
                if (productCategory.CategoryMedia != null)
                {
                    var file = await _fileAppService.DownloadFileByIdAsync(productCategory.CategoryMedia.Id);
                    res.UploadedMedia = ObjectMapper.Map<FileDto, MediaDto>(file);
                }

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateProductCategoryDto dto)
        {
            try
            {
                var productCategory = await _productCategoryRepository.GetAsync(dto.Id);

                var updatedProductCategory = ObjectMapper.Map<CreateUpdateProductCategoryDto, Models.ProductCategory>(dto);
                updatedProductCategory.ConcurrencyStamp = productCategory.ConcurrencyStamp;

                // Add To File Table
                if (dto.CategoryMedia != null)
                {
                    if (productCategory.CategoryMedia != null)
                        await _fileAppService.DeleteDownload(productCategory.CategoryMedia.Id);

                    var file = ObjectMapper.Map<UserFileDto, CreateFileDto>(dto.CategoryMedia);
                    var uploadedFile = await _fileAppService.InsertFileAsync(file);

                    updatedProductCategory.CategoryMedia = new Media
                    {
                        Id = uploadedFile.Id,
                        Name = uploadedFile.Filename
                    };
                }

                await _productCategoryRepository.UpdateAsync(updatedProductCategory, true);
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
                var productCategory = await _productCategoryRepository.GetAsync(id);
                await _productCategoryRepository.DeleteAsync(productCategory);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
