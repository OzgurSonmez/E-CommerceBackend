using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.BasketProduct
{
    public class AddProductToBasketRequest
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int IsSelected { get; set; }
    }
}
