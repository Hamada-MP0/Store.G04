using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.G04.Core.Services.Contract;
using System.Text;

namespace Store.G04.Apis.Attriibute
{

//    Attribute: Allows you to use the class as an attribute above an action or a controller.
//IAsyncActionFilter: Allows the class to execute code both before and after an action is executed.

    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CashedAttribute(int ExpireTime)
        {
            _expireTime = ExpireTime;
        }

        //The `CashedAttribute` allows for checking whether a response exists in the Redis cache;
        //if it does not, it lets the controller execute its logic and then stores the resulting response.

        //This is an object that contains information about the current request. >>ActionExecutingContext
        //A delegate is a variable that points to a method you can call.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var casheService= context.HttpContext.RequestServices.GetRequiredService<ICasheService>();

            var casheley = GenerateCasheKeyFromREquest(context.HttpContext.Request);
            
           var casheRes = await casheService.GetCasheKeyAsync(casheley);

            if(!string.IsNullOrEmpty(casheRes) )
            {
                var contentResualt = new ContentResult()
                {
                    Content=casheRes,
                    ContentType="application/json",
                    StatusCode=200
                };
                context.Result= contentResualt;
                return;
            }

            var excutedContext = await next();

            if(excutedContext.Result is OkObjectResult Response)
            {
                 await casheService.SetCasheKeyAsync(casheley, Response, TimeSpan.FromSeconds(_expireTime));
            }

        }


        private static string GenerateCasheKeyFromREquest(HttpRequest Request)

        {
            // 
            var casheKey = new StringBuilder();
            casheKey.Append($"{Request.Path}");

            foreach (var (key, value) in Request.Query.OrderBy(x => x.Key))
            {
                casheKey.Append($"|{key}-{value}");
            }
            return casheKey.ToString();

        }


    }
}
