using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Dtos.Baskets
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
