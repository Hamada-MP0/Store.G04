using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Dtos.Baskets
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public int Quantity { get; set; }

    }
}
