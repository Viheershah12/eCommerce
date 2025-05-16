using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Dtos.Mpesa
{
    public class MpesaStkPushRequestDto
    {
        public long PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string AccountReference { get; set; }
        public string TransactionDescription { get; set; }
    }

    public class MpesaStkPushPayload
    {
        public long BusinessShortCode { get; set; }
        public string Password { get; set; }
        public string Timestamp { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public long PartyA { get; set; }
        public long PartyB { get; set; }
        public long PhoneNumber { get; set; }
        public string CallBackURL { get; set; }
        public string AccountReference { get; set; }
        public string TransactionDesc { get; set; }
    }
}
