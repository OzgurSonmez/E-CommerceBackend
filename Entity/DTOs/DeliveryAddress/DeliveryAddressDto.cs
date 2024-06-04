using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.DeliveryAddress
{
    public class DeliveryAddressDto
    {
        public string FullName { get; set; }
        public string DeliveryAddressDetail { get; set; }
        public int DeliveryAddressId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
