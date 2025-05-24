using AutoMapper;
using Order.Dtos.OrderTransaction;
using Order.Web.Models.Order;
using Volo.Abp.AutoMapper;

namespace Order.Web;

public class OrderWebAutoMapperProfile : Profile
{
    public OrderWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        
        CreateMap<OrderDetailDto, OrderViewModel>();
        CreateMap<OrderDetailDto.OrderItemDto, OrderViewModel.OrderItemViewModel>();
        CreateMap<OrderDetailDto.CustomerDetailDto, OrderViewModel.CustomerDetailViewModel>();

        CreateMap<OrderDetailDto.OrderNoteDto, OrderViewModel.OrderNoteViewModel>()
            .Ignore(x => x.OrderNoteTypeName);

        CreateMap<OrderDetailDto.PaymentTransactionDto, OrderViewModel.PaymentTransactionViewModel>();
        CreateMap<OrderDetailDto.MpesaTransactionDto, OrderViewModel.MpesaTransactionViewModel>();

        CreateMap<Pages.Order.AddOrderNoteModalModel.CreateViewModel, CreateUpdateOrderNoteDto>()
            .Ignore(x => x.OrderId)
            .ReverseMap();

        CreateMap<Pages.Order.EditOrderNoteModalModel.EditViewModel, CreateUpdateOrderNoteDto>()
            .Ignore(x => x.OrderId)
            .ReverseMap();
    }
}
