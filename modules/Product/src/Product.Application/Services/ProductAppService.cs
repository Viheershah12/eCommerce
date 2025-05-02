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
using Customer.Interfaces;
using Abp.eCommerce.Models;
using Management.Dtos.File;
using Product.Dtos.ProductCategory;
using Management.Interfaces;
using Product.Dtos.Common;
using Volo.Abp.ObjectMapping;
using static Product.Models.Product;
using Product.ProductCategory;

namespace Product.Services
{
    public class ProductAppService : ProductsAppService, IProductAppService
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        private readonly ProductManager _productManager;
        private readonly IProductTagRepository _productTagRepository;
        private readonly Customer.Interfaces.ICommonAppService _customerCommonAppService;
        private readonly IFileAppService _fileAppService;
        private readonly ICustomerGroupAppService _customerGroupAppService;
        private readonly IProductCategoryRepository _productCategoryRepository;
        #endregion

        #region CTOR
        public ProductAppService(
            IProductRepository productRepository,
            ProductManager productManager,
            IProductTagRepository productTagRepository,
            Customer.Interfaces.ICommonAppService customerCommonAppService,
            IFileAppService fileAppService,
            ICustomerGroupAppService customerGroupAppService,
            IProductCategoryRepository productCategoryRepository
        )
        {
            _productRepository = productRepository;
            _productManager = productManager;   
            _productTagRepository = productTagRepository;   
            _customerCommonAppService = customerCommonAppService;
            _fileAppService = fileAppService;
            _customerGroupAppService = customerGroupAppService;
            _productCategoryRepository = productCategoryRepository; 
        }
        #endregion

        #region Product
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

        public async Task<PagedResultDto<StoreProductDto>> GetListByCategoryAsync(Dtos.Product.GetProductCategoryListDto dto)
        {
            try
            {
                if (dto.Category == null)
                {
                    var category = (await _productCategoryRepository.GetListAsync())
                        .OrderBy(x => x.DisplayOrder)
                        .FirstOrDefault();

                    dto.Category = category?.Id;
                }

                var (items, totalCount) = await _productManager.GetProductByCategoryListing(dto);
                var list = new List<StoreProductDto>();

                foreach (var item in items)
                {
                    var prod = ObjectMapper.Map<Models.Product, StoreProductDto>(item);

                    // Get Files
                    if (item.Media != null)
                    {
                        var files = await _fileAppService.DownloadMultipleFileByIdAsync(item.Media.Select(x => x.Id).ToList());
                        prod.Media = ObjectMapper.Map<List<FileDto>, List<UserFileDto>>(files);
                    }

                    list.Add(prod);
                }

                return new PagedResultDto<StoreProductDto>(totalCount, list);
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

                // Get Customer Group
                var groupIds = dto.LimitedToCustomerGroups.Select(x => x.Id).ToList();
                var groups = await _customerCommonAppService.GetCutomerGroupByIdAsync(groupIds);

                productDto.LimitedToCustomerGroups = groups.Select(x => new Models.Product.CustomerGroup
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArray();

                // Upload File
                if (dto.Media != null)
                {
                    var files = ObjectMapper.Map<List<UserFileDto>, List<CreateFileDto>>(dto.Media);
                    var uploadedFiles = await _fileAppService.InsertMultipleFilesAsync(files);

                    productDto.Media = uploadedFiles.Select(x => new Media
                    {
                        Id = x.Id,
                        Name = x.Filename
                    }).ToList();
                }

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

                // Get File
                if (product.Media != null)
                {
                    var files = await _fileAppService.DownloadMultipleFileByIdAsync(product.Media.Select(x => x.Id).ToList());
                    res.UploadedMedia = ObjectMapper.Map<List<FileDto>, List<UserFileDto>>(files);
                }

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
                updatedProduct.TierPrices = product.TierPrices;

                // Get Product Tags
                var tagIds = dto.ProductTags.Select(x => x.Id).ToList();
                var tags = await _productTagRepository.GetListAsync(x => tagIds.Contains(x.Id));

                updatedProduct.ProductTags = tags.Select(x => new Models.Product.ProductTag
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArray();

                // Get Customer Group
                var groupIds = dto.LimitedToCustomerGroups.Select(x => x.Id).ToList();
                var groups = await _customerCommonAppService.GetCutomerGroupByIdAsync(groupIds);

                updatedProduct.LimitedToCustomerGroups = groups.Select(x => new Models.Product.CustomerGroup
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArray();

                // Upload Files
                if (dto.Media != null)
                {
                    var files = ObjectMapper.Map<List<UserFileDto>, List<CreateFileDto>>(dto.Media);
                    var uploadedFiles = await _fileAppService.InsertMultipleFilesAsync(files);

                    if (product.Media != null)
                    {
                        updatedProduct.Media = [];
                        updatedProduct.Media.AddRange(product.Media);
                    }
                    else
                    {
                        updatedProduct.Media = [];
                    }

                    updatedProduct.Media.AddRange(uploadedFiles.Select(x => new Media
                    {
                        Id = x.Id,
                        Name = x.Filename
                    }).ToList());
                }
                else
                {
                    updatedProduct.Media = product.Media;
                }

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
        #endregion 

        #region Media
        public async Task DeleteProductMediaAsync(DeleteProductMedia dto)
        {
            try
            {
                var product = await _productRepository.GetAsync(dto.Id);    

                if (product.Media != null)
                {
                    product.Media.RemoveAll(x => x.Id == dto.FileId);

                    await _fileAppService.DeleteDownload(dto.FileId);
                    await _productRepository.UpdateAsync(product);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        #endregion 

        #region Tier Price
        public async Task CreateTierPriceAsync(CreateUpdateProductTierPriceDto dto)
        {
            try
            {
                var product = await _productRepository.GetAsync(dto.ProductId);
                product.TierPrices.RemoveAll(x => x.CustomerGroupId == dto.CustomerGroupId);

                var customerGroup = await _customerGroupAppService.GetAsync(dto.CustomerGroupId);
                product.TierPrices.Add(new Models.Product.TeirPrice
                {
                    Id = Guid.NewGuid(),
                    CustomerGroupId = dto.CustomerGroupId,
                    CustomerGroup = customerGroup.Name,
                    Price = dto.Price,
                    MinimumQuantity = dto.MinimumQuantity
                });

                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<TierPriceDto> GetTierPriceAsync(GetTierPriceDto dto)
        {
            try
            {
                var product = await _productRepository.GetAsync(dto.ProductId);
                var tierPrice = product.TierPrices.FirstOrDefault(x => x.Id == dto.Id);

                if (tierPrice == null)
                    throw new UserFriendlyException("The Error");

                var res = new TierPriceDto
                {
                    Id = tierPrice.Id,
                    CustomerGroupId = tierPrice.CustomerGroupId,
                    CustomerGroup = tierPrice.CustomerGroup,
                    Price = tierPrice.Price,
                    MinimumQuantity = tierPrice.MinimumQuantity
                };

                return res;
            }
            catch (UserFriendlyException ex)
            {
                throw new BusinessException(ex.Code, ex.Message, ex.Details);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateTierPriceAsync(CreateUpdateProductTierPriceDto dto)
        {
            try
            {
                var product = await _productRepository.GetAsync(dto.ProductId);
                var tierPrice = product.TierPrices.FirstOrDefault(x => x.Id == dto.Id);

                if (tierPrice != null)
                {
                    product.TierPrices.RemoveAll(x => x.Id == dto.Id);
                    var customerGroup = await _customerGroupAppService.GetAsync(dto.CustomerGroupId);

                    product.TierPrices.Add(new Models.Product.TeirPrice
                    {
                        Id = dto.Id,
                        CustomerGroupId = dto.CustomerGroupId,
                        CustomerGroup = customerGroup.Name,
                        Price = dto.Price,
                        MinimumQuantity = dto.MinimumQuantity
                    });

                    await _productRepository.UpdateAsync(product);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task DeleteTierPriceAsync(DeleteTierPriceDto dto)
        {
            try
            {
                var product = await _productRepository.GetAsync(dto.ProductId);
                product.TierPrices.RemoveAll(x => x.Id == dto.Id);

                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        #endregion

        #region Similar Products
        public async Task<List<StoreProductDto>> GetSimilarProductAsync(List<Guid> tagIds, int limit = 10)
        {
            try
            {
                var products = (await _productRepository.GetListAsync(
                    x => x.ProductTags.Any(tag => tagIds.Contains(tag.Id))
                ))
                .Take(limit)
                .ToList();

                var res = new List<StoreProductDto>();
                foreach (var item in products)
                {
                    var prod = ObjectMapper.Map<Models.Product, StoreProductDto>(item);

                    // Get Files
                    if (item.Media != null)
                    {
                        var files = await _fileAppService.DownloadMultipleFileByIdAsync(item.Media.Select(x => x.Id).ToList());
                        prod.Media = ObjectMapper.Map<List<FileDto>, List<UserFileDto>>(files);
                    }

                    res.Add(prod);
                }

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        #endregion 
    }
}
