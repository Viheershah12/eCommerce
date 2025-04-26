using Product.Dtos.Product;
using Product.Interfaces;
using Product.Products;
using Product.ProductTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Product.Services
{
    public class ProductAppService : ProductsAppService, IProductAppService
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        private readonly ProductManager _productManager;
        private readonly IProductTagRepository _productTagRepository;
        #endregion

        #region CTOR
        public ProductAppService(
            IProductRepository productRepository,
            ProductManager productManager,
            IProductTagRepository productTagRepository
        )
        {
            _productRepository = productRepository;
            _productManager = productManager;   
            _productTagRepository = productTagRepository;   
        }
        #endregion 

        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto dto)
        {
            try
            {
                var (items, totalCount) = await _productManager.GetProductListing(dto);
                var list = ObjectMapper.Map<List<Models.Product>, List<ProductDto>>(items);

                return new PagedResultDto<ProductDto>(totalCount, list);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateProductDto dto) 
        {
            try
            {
                var productDto = ObjectMapper.Map<CreateUpdateProductDto, Models.Product>(dto);

                // Get Product Tags
                var tagIds = dto.ProductTags.Select(x => x.Id).ToList();
                var tags = await _productTagRepository.GetListAsync(x => tagIds.Contains(x.Id));

                productDto.ProductTags = tags.Select(x => new Models.Product.ProductTag
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArray();

                var product = await _productRepository.InsertAsync(productDto);

                return product.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateProductDto> GetAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.Product, CreateUpdateProductDto>(product);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateProductDto dto)
        {
            try
            {
                var product = await _productRepository.GetAsync(dto.Id);

                var updatedProduct = ObjectMapper.Map<CreateUpdateProductDto, Models.Product>(dto);
                updatedProduct.ConcurrencyStamp = product.ConcurrencyStamp;

                // Get Product Tags
                var tagIds = dto.ProductTags.Select(x => x.Id).ToList();
                var tags = await _productTagRepository.GetListAsync(x => tagIds.Contains(x.Id));

                updatedProduct.ProductTags = tags.Select(x => new Models.Product.ProductTag
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArray();

                await _productRepository.UpdateAsync(updatedProduct);
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
                var product = await _productRepository.GetAsync(id);
                await _productRepository.DeleteAsync(product);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
