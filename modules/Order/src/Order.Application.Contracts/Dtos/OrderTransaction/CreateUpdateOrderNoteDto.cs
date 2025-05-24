using Abp.eCommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Order.Dtos.OrderTransaction
{
    public class CreateUpdateOrderNoteDto : IdOrderIdModel
    {
        public OrderNoteType OrderNoteType { get; set; }

        public string Note { get; set; }

        public string CreatorName { get; set; }
    }

    public class IdOrderIdModel : CreationAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
    }
}
