﻿using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Order.Dtos.OrderTransaction
{
    public class CreateUpdateOrderDto : BaseIdModel
    {
        public AddressTypeEnum SelectedAddress { get; set; }

        public CustomerDetailDto Customer { get; set; }

        public class CustomerDetailDto : BaseIdModel
        {
            public string CustomerName { get; set; }

            public UserAddress? DeliveryAddress { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public string? HomePhoneNumber { get; set; }

            public Gender? Gender { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public IdentificationType? IdentificationType { get; set; }

            public string? IdentificationNo { get; set; }
        }

        #region Order Items
        public List<OrderItemDto> OrderItems { get; set; } = [];

        public partial class OrderItemDto : BaseIdModel
        {
            public Guid ProductId { get; set; }

            public string ProductName { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }

            public decimal Total => Quantity * Price;
        }
        #endregion 

        public decimal TotalAmount => OrderItems.Sum(x => x.Total);

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.UnPaid;

        public PaymentMethodEnum? PaymentMethod { get; set; }

        public string? PaymentMethodSystemName { get; set; }

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Pending;

        public DateTime? PaidDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        #region Order Note
        public List<OrderNoteDto> OrderNotes { get; set; } = [];

        public class OrderNoteDto : CreationAuditedEntityDto<Guid>
        {
            public OrderNoteType OrderNoteType { get; set; }

            public string Note { get; set; }

            public string CreatorName { get; set; }
        }
        #endregion     
    }
}
