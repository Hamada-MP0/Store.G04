//using Store.G04.Core;
//using Store.G04.Core.Entity;
//using Store.G04.Core.Entity.Order;
//using Store.G04.Core.Repositories.Contract;
//using Store.G04.Core.Services.Contract;
//using Store.G04.Repository.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Store.G04.Core.Specifications;
//using Stripe;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Address = Store.G04.Core.Entity.Order.Address;
//using Product = Store.G04.Core.Entity.Product;
//using Stripe.Climate;


//namespace Store.G04.Service.Services.Orders
//{
//    public class s : IOrderService
//    {

//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IBasketRepositories _basketService;
//        private readonly IPaymentService _paymentService;

//        public s(IUnitOfWork unitOfWork, IBasketRepositories basketService, IPaymentService payment)
//        {
//            _unitOfWork = unitOfWork;
//            _basketService = basketService;
//            _paymentService = payment;
//        }

//        public async Task<Store.G04.Core.Entity.Order.Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
//        {
//            var basket = await _basketService.GetBasketAsync(basketId);
//            if (basket is null) return null;

//            var orderItems = new List<OrderItem>();

//            if (basket.Items.Count() > 0)
//            {
//                foreach (var item in basket.Items)
//                {
//                    var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);
//                    var ProductOrderedItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
//                    var orderItem = new OrderItem(ProductOrderedItem, product.price, item.Quantity);

//                    orderItems.Add(orderItem);
//                }
//            }
//            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(deliveryMethodId);

//            var subTotal = orderItems.Sum(I => I.Price * I.Quantity);

//            //TODO
//            if (!string.IsNullOrEmpty(basket.PaymentIntentId))
//            {
//                var spec = new OrderSpecificationWithPayment(basket.PaymentIntentId);
//                var ExOrder = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
//                _unitOfWork.Repository<Order, int>().Delete(ExOrder);
//            }
//            var basketDto = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);

//            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal, basketDto.PaymentIntentId);


//            await _unitOfWork.Repository<Order, int>().AddAsync(order);

//            var result = await _unitOfWork.CompletelyAsync();

//            if (result <= 0) return null;

//            return order;
//        }

//        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
//        {
//            var spec = new OrderSpecification(buyerEmail, orderId);
//            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
//            if (order is null) return null;

//            return order;
//        }

//        public async Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail)
//        {
//            var spec = new OrderSpecification(buyerEmail);
//            var orders = await _unitOfWork.Repository<Order, int>().GetAllWithSpecAsync(spec);

//            if (orders is null) return null;
//            return orders;
//        }
//    }
//}
