using Store.G04.Core.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Entity
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; } 
        public int? DeliveryMethod { get; set; }
        public string? PaymentIntentId { get; set; }
        // to determind your Authen Ex personal card
        public string? ClientSecret   { get; set; }
    }
}
