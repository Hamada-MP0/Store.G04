using Microsoft.VisualBasic;
using StackExchange.Redis;
using Store.G04.Core.Entity;
using Store.G04.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.G04.Service.Services.Cashe
{


    //It stores temporary data so that the application can retrieve it quickly, rather than having to recalculate it or request it from its source every single time.
    public class CasheService : ICasheService
    {
        private readonly IDatabase _database;
        //IConnectionMultiplexer: Manages the connection to Redis.

        //GetDatabase() : Retrieves the interface for interacting with a Redis database.
        //    IDatabase: Allows you to read, write, and delete data.




        public CasheService( IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        //A "key"—the name that Redis uses to access the stored value.
        public async Task<string> GetCasheKeyAsync(string key)
        {
             var CasheResponse = await  _database.StringGetAsync(key);
            if (CasheResponse.IsNullOrEmpty) return null;
            return CasheResponse.ToString();
        }
        //The response can be any object, such as a CustomerBasket.
        //CamelCase makes property names start with lowercase letters.
        public async Task SetCasheKeyAsync(string key, object response, TimeSpan expiretime)
        {
            if (response is null) return;
            // determind formate Properties name After the Convert From Object To Json
            //{                "Id": "basket1","TotalPrice": 100}Withoout
            //{  "id": "basket1", "totalPrice": 100} With
            var Options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var REScasheJson = JsonSerializer.Serialize(response, Options);
          await _database.StringSetAsync(key, REScasheJson, expiretime);
   
        }
    }
}
