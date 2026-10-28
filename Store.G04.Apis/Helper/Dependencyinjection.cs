using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.G04.Apis.Error;
using Store.G04.Core;
using Store.G04.Core.Entity.Identity;
using Store.G04.Core.Mapping.Basket;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Core.Services.Contract;
using Store.G04.Repository;
using Store.G04.Repository.Data.Context;
using Store.G04.Repository.Identity.Context;
using Store.G04.Repository.Repositories;
using Store.G04.Service.Services.Cashe;
using Store.G04.Service.Services.Products;
using Store.G04.Service.Services.Service;
using Store.G04.Service.Services.Tokens;

namespace Store.G04.Apis.Helper
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services , IConfiguration configuration)
        {
            // here we can add all the services that we want to use in our application, and we can also configure them if needed.
            services.AddApplicationServices();
            services.AddSwaggerServices();
            services.AddDbContextServices(configuration);
            services.AddUserDefinedServices();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponseFactoryService(configuration );
            services.AddRedisServices(configuration);
            services.AddIdentityeSrvices();

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {// this method is used to add the application services that are required for the application to run, such as controllers, views, etc.
            services.AddControllers();

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // this method is used to add the swagger services that are required for the application to run, such as swagger UI, swagger generator, etc.
            services.AddEndpointsApiExplorer();
           services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddDbContextServices(this IServiceCollection services , IConfiguration configuration)
        {
            // Database
            // this method is used to add the database services that are required for the application to run, such as DbContext, connection string, etc.
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection"));
            });
            services.AddDbContext<StoreIdentityDBContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection"));
            });
            return services;
        }

        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            //  this method is used to add the user defined services that are required for the application to run, such as repositories, services, etc.
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBasketRepositories, BasketRepository>();
            services.AddScoped<ICasheService, CasheService>();
            return services;
        }


        private static IServiceCollection AddAutoMapperService (this IServiceCollection services , IConfiguration configuration)
        {
            // this method is used to add the automapper services that are required for the application to run, such as mapping profiles, etc.
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new ProductProfile(configuration));
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new BasketProfile());
            });
            return services;
        }



        private static IServiceCollection ConfigureInvalidModelStateResponseFactoryService(this IServiceCollection services, IConfiguration configuration)
        {
            // to deals with the model validation error and return a custom response instead of the default 400 Bad Request response
            // this method is used to configure the invalid model state response factory that is required for the application to run, such as custom error response, etc.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var Errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                       .SelectMany(p => p.Value.Errors)
                       .Select(E => E.ErrorMessage)
                       .ToArray();

                    var Response = new ApiValidationErrorResponse()
                    {
                        Errors = Errors
                    };
                    return new BadRequestObjectResult(Response)
                    {
                        ContentTypes = { "application/json" }
                    };


                };
            });


            return services;
        }





        private static IServiceCollection AddRedisServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
             var connect=   configuration.GetConnectionString("Radis");
            
            return ConnectionMultiplexer.Connect(connect);
            });

            return services;
        }



        private static IServiceCollection AddIdentityeSrvices(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDBContext>(); 

            return services;
        }




    }
}
