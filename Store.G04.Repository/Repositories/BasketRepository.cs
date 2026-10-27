using StackExchange.Redis;
using Store.G04.Core.Entity;
using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Store.G04.Repository.Repositories
{
    public class BasketRepository : IBasketRepositories
    {
        // Idatabase is interface from redis library

        private readonly IDatabase _database;

        
        public BasketRepository(IConnectionMultiplexer redis)
        {
          _database =  redis.GetDatabase();


        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
         return await  _database.KeyDeleteAsync(basketId);
         
        }
        // from json to  object >> Des
        public async Task<CustomerBasket> GetBasketByAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>((string)basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created =await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (created is false) return null;
            return await GetBasketByAsync(basket.Id);
        }
    }
}
