
using Store.G04.Core.Entity.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(
            string buyerEmail,
            string basketId,
            int deliveryMethodId,
            Address shippingAddress);

        Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(
            string buyerEmail);

        Task<Order?> GetOrderByIdForSpecificUserAsync(
            string buyerEmail,
            int orderId);
    }
}
