
using Store.G04.Core;
using Store.G04.Core.Entity;
using Store.G04.Core.Entity.Order;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications.Orders;

namespace Store.G04.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepositories _basketService;

        public OrderService(
            IUnitOfWork unitOfWork,
            IBasketRepositories basketService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }

        public async Task<Order?> CreateOrderAsync(
            string buyerEmail,
            string basketId,
            int deliveryMethodId,
            Address shippingAddress)
        {
            // 1. Get basket from Redis
            var basket = await _basketService.GetBasketByAsync(basketId);

            if (basket is null)
                throw new InvalidOperationException(
                    $"Basket '{basketId}' was not found in Redis.");

            if (basket.Items is null || !basket.Items.Any())
                throw new InvalidOperationException(
                    $"Basket '{basketId}' contains no items.");

            var orderItems = new List<OrderItems>();

            // 2. Get products and create order items
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork
                    .Repository<Product, int>()
                    .GetByIdAsync(item.Id);

                if (product is null)
                    throw new InvalidOperationException(
                        $"Product with ID '{item.Id}' was not found.");

                var orderedProduct = new ProductItemsOrder(
                    product.Id,
                    product.Name,
                    product.PictureUrl);

                var orderItem = new OrderItems(
                    item.Quantity,
                    product.Price,
                    orderedProduct);

                orderItems.Add(orderItem);
            }

            // 3. Get delivery method
            var deliveryMethod = await _unitOfWork
                .Repository<DeliveryMethod, int>()
                .GetByIdAsync(deliveryMethodId);

            if (deliveryMethod is null)
                throw new InvalidOperationException(
                    $"Delivery method with ID '{deliveryMethodId}' was not found.");

            // 4. Calculate subtotal
            var subTotal = orderItems.Sum(
                item => item.Price * item.Quentity);

            // 5. Create order
            var order = new Order
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = shippingAddress,
                DeliveryMethod = deliveryMethod,
                DeliveryMethodId = deliveryMethodId,
                Items = orderItems,
                SubTotal = subTotal,
                PaymentIntentId = string.Empty
            };

            // 6. Save order to database
            await _unitOfWork
                .Repository<Order, int>()
                .AddAsync(order);

            await _unitOfWork.CommitAsync();

            return order;
        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(
            string buyerEmail,
            int orderId)
        {
            var spec = new OrderSpecification(buyerEmail, orderId);

            return await _unitOfWork
                .Repository<Order, int>()
                .GetByIdWithSpecAsync(spec);
        }

        public async Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(
            string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);

            return await _unitOfWork
                .Repository<Order, int>()
                .GetAllًWithSpecAsync(spec);
        }
    }
}
