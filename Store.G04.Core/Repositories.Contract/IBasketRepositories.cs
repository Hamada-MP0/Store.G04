using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IBasketRepositories
    {
        Task<CustomerBasket?> GetBasketByAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
 