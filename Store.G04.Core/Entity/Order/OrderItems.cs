using Store.G04.Core.Entity.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Entity.Order
{
    public class OrderItems:BaseEntity<int>
    {
        public OrderItems()
        {
        }


        public OrderItems(int quentity, decimal price , ProductItemsOrder product)
        {
            Quentity = quentity;
            Price = price;
            Product = product;
        }
        public ProductItemsOrder Product { get; set; }
        public decimal Price { get; set; }

        public int Quentity { get; set; }

     
    }
}
