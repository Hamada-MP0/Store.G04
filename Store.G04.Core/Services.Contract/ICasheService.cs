using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Services.Contract
{
    public interface ICasheService
    {
        Task SetCasheKeyAsync(string key, object response, TimeSpan expiretime);
        Task<string> GetCasheKeyAsync(string key);

    }
}
