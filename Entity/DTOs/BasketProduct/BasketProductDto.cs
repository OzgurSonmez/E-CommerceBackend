using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.BasketProduct
{
    public class BasketProductDto
    {
        public int BasketId { get; set; }
        public string BrandName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductIsSelected { get; set; }
    }
}
