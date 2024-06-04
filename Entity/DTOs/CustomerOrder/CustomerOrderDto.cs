using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.CustomerOrder
{
    public class CustomerOrderDto
    {
        public int CustomerOrderId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string FullName { get; set; }
        public string DeliveryAddressDetail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
