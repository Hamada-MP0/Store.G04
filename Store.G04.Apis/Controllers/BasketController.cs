using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;
using Store.G04.Core.Dtos.Baskets;
using Store.G04.Core.Entity;
using Store.G04.Core.Repositories.Contract;

namespace Store.G04.Apis.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepositories _basketRepositories;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepositories basketRepositories , IMapper mapper)
        {
            _basketRepositories = basketRepositories;
            _mapper = mapper;
        }

        //ActionResult<CustomerBasket> allows the method to return either a NotFound response or the requested data of type CustomerBasket.
        [HttpGet]//Api/basket
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            if (id == null) return BadRequest(new ApiErrorResponse(400, " Invalid Id !!"));
            var baskID = await _basketRepositories.GetBasketByAsync(id);
            if (baskID == null) baskID = new CustomerBasket() { Id = id };
            return Ok(baskID);
                
         }

        // We use AutoMapper to convert CustomerBasketDto (received from the client)
        // into a CustomerBasket object that the application uses internally.

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(
    [FromBody] CustomerBasketDto basketModet)
        {
          var basket = await  _basketRepositories.UpdateBasketAsync(_mapper.Map<CustomerBasket>(basketModet));
            if (basket == null) return BadRequest(new ApiErrorResponse(400));

            return Ok(basket);

        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            if (id == null)
                return;

            await _basketRepositories.DeleteBasketAsync(id);
        }
    }
}
