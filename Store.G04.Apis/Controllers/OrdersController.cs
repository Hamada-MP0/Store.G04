using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;
using Store.G04.Core;
using Store.G04.Core.Dtos.Orders;
using Store.G04.Core.Entity.Order;
using Store.G04.Core.Services.Contract;
using System.Security.Claims;
using OrderAddress = Store.G04.Core.Entity.Order.Address;
namespace Store.G04.Apis.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpPost]
    
        public async Task<IActionResult> CreateOrder(OrderDto model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var address = _mapper.Map<OrderAddress>(model.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(userEmail, model.BasketId, model.DeliveryMethodId, address);

            if (order is null)
            {
                return BadRequest(new
                {
                    message = "Order creation failed. Check basket, products, and delivery method."
                });
            }

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

       
        [HttpGet]

        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(value: new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var orders = await _orderService.GetOrdersForSpecificUserAsync(userEmail);
            if (orders is null) return BadRequest(error: new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(orders));
        }

        
        [HttpGet("{orderId}")]

        public async Task<IActionResult> GetOrdersForSpecificUser(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var order = await _orderService.GetOrderByIdForSpecificUserAsync(userEmail, orderId);

            if (order is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }




        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

            if (deliveryMethods is null) //عشان لو رجع null ميبقاش فيه اكسبشن
                return BadRequest(error: new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(deliveryMethods);
        }




  
    }
}
