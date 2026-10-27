using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Entity
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } 
    }
}
