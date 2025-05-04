using Order.Dtos.ShoppingCart;
using Order.Interfaces;
using Order.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Order.Services
{
    public class ShoppingCartAppService : OrderAppService, IShoppingCartAppService
    {
        #region Fields
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ShoppingCartManager _shoppingCartManager;
        #endregion

        #region CTOR
        public ShoppingCartAppService(
            IShoppingCartRepository shoppingCartRepository,
            ShoppingCartManager shoppingCartManager
        )
        {
            _shoppingCartRepository = shoppingCartRepository;   
            _shoppingCartManager = shoppingCartManager;
        }
        #endregion 

        public async Task<PagedResultDto<ShoppingCartDto>> GetListAsync(GetShoppingCartListDto dto)
        {
            try
            {
                var (items, totalCount) = await _shoppingCartManager.GetShoppingCartListing(dto);
                var list = ObjectMapper.Map<List<Models.ShoppingCart>, List<ShoppingCartDto>>(items);

                return new PagedResultDto<ShoppingCartDto>(totalCount, list);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateShoppingCartDto dto)
        {
            try
            {
                var model = ObjectMapper.Map<CreateUpdateShoppingCartDto, Models.ShoppingCart>(dto);
                var shoppingCart = await _shoppingCartRepository.InsertAsync(model);

                return shoppingCart.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateShoppingCartDto> GetAsync(Guid id)
        {
            try
            {
                var shoppingCart = await _shoppingCartRepository.GetAsync(id);  
                var res = ObjectMapper.Map<Models.ShoppingCart, CreateUpdateShoppingCartDto>(shoppingCart);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task AddShoppingCartItemAsync(CreateUpdateShoppingCartItemDto dto)
        {
            try
            {
                if (CurrentUser.Id != null && !string.IsNullOrEmpty(CurrentUser.UserName))
                {
                    var shoppingCart = await _shoppingCartRepository.FirstOrDefaultAsync(x => x.UserId == CurrentUser.Id);

                    if (shoppingCart != null)
                    {
                        var cartItem = ObjectMapper.Map<CreateUpdateShoppingCartItemDto, Models.ShoppingCart.CartItem>(dto);
                        var existingCartItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == dto.ProductId);

                        if (existingCartItem != null)
                        {
                            existingCartItem.Quantity += dto.Quantity;
                            existingCartItem.UpdatedOn = DateTime.Now;

                            shoppingCart.Items.RemoveAll(x => x.ProductId == dto.ProductId);
                            shoppingCart.Items.Add(existingCartItem);
                        }
                        else
                        {
                            shoppingCart.Items.Add(cartItem);
                        }

                        await _shoppingCartRepository.UpdateAsync(shoppingCart);
                    }
                    else
                    {
                        var newCart = new Models.ShoppingCart
                        {
                            UserId = CurrentUser.Id.Value,
                            Username = CurrentUser.UserName,
                            Items =
                            [
                                ObjectMapper.Map<CreateUpdateShoppingCartItemDto, Models.ShoppingCart.CartItem>(dto)
                            ]
                        };

                        await _shoppingCartRepository.InsertAsync(newCart);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateShoppingCartDto dto)
        {
            try
            {
                var shoppingCart = await _shoppingCartRepository.GetAsync(dto.Id);

                var updatedShoppingCart = ObjectMapper.Map<CreateUpdateShoppingCartDto, Models.ShoppingCart>(dto);
                updatedShoppingCart.ConcurrencyStamp = shoppingCart.ConcurrencyStamp;

                await _shoppingCartRepository.UpdateAsync(updatedShoppingCart);
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
                var shoppingCart = await _shoppingCartRepository.GetAsync(id);
                await _shoppingCartRepository.DeleteAsync(shoppingCart);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
