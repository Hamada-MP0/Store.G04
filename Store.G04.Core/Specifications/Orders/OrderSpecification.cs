using Store.G04.Core.Entity.Order;
using Store.G04.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Specifications.Orders
{
    public class OrderSpecification : BaseSpecification<Order, int>
    {

    


        public OrderSpecification(string buyerEmail, int orderId)
                : base(O => O.BuyerEmail == buyerEmail && O.Id == orderId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }

        public OrderSpecification(string buyerEmail)
                 : base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
