using Order.Dtos.ShoppingCart;
using Order.Dtos.WishList;
using Order.Interfaces;
using Order.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Order.Services
{
    public class WishListAppService : OrderAppService, IWishListAppService
    {
        #region Fields
        private readonly IWishListRepository _wishListRepository;
        private readonly WishListManager _wishListManager;
        #endregion

        #region CTOR
        public WishListAppService(
            IWishListRepository wishListRepository,
            WishListManager wishListManager
        )
        {
            _wishListRepository = wishListRepository;
            _wishListManager = wishListManager;
        }
        #endregion 

        public async Task<PagedResultDto<WishListDto>> GetListAsync(GetWishListListingDto dto)
        {
            try
            {
                var (items, totalCount) = await _wishListManager.GetWishListListing(dto);
                var list = ObjectMapper.Map<List<Models.WishList>, List<WishListDto>>(items);

                return new PagedResultDto<WishListDto>(totalCount, list);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateWishListDto dto)
        {
            try
            {
                var model = ObjectMapper.Map<CreateUpdateWishListDto, Models.WishList>(dto);        
                var wishList = await _wishListRepository.InsertAsync(model);    

                return wishList.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateWishListDto> GetAsync(Guid id)
        {
            try
            {
                var wishList = await _wishListRepository.GetAsync(id);  
                var res = ObjectMapper.Map<Models.WishList, CreateUpdateWishListDto>(wishList); 

                return res; 
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task AddWishlistItemAsync(CreateUpdateWishlistItemDto dto)
        {
            try
            {
                if (CurrentUser.Id != null && !string.IsNullOrEmpty(CurrentUser.UserName))
                {
                    var wishList = await _wishListRepository.FirstOrDefaultAsync(x => x.UserId == CurrentUser.Id.Value);

                    if (wishList != null)
                    {
                        var wishListItem = ObjectMapper.Map<CreateUpdateWishlistItemDto, Models.WishList.WishlistItem>(dto);

                        wishList.Items.RemoveAll(x => x.ProductId == dto.ProductId);
                        wishList.Items.Add(wishListItem);

                        await _wishListRepository.UpdateAsync(wishList);
                    }
                    else
                    {
                        var newWishlist = new Models.WishList
                        {
                            UserId = CurrentUser.Id.Value,
                            Username = CurrentUser.UserName,
                            Items =
                            [
                                ObjectMapper.Map<CreateUpdateWishlistItemDto, Models.WishList.WishlistItem>(dto)
                            ]
                        };

                        await _wishListRepository.InsertAsync(newWishlist);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateWishListDto dto)
        {
            try
            {
                var wishList = await _wishListRepository.GetAsync(dto.Id);

                var updatedWishList = ObjectMapper.Map<CreateUpdateWishListDto, Models.WishList>(dto);
                updatedWishList.ConcurrencyStamp = wishList.ConcurrencyStamp;   

                await _wishListRepository.UpdateAsync(updatedWishList);
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
                var wishList = await _wishListRepository.GetAsync(id);
                await _wishListRepository.DeleteAsync(wishList);    
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
