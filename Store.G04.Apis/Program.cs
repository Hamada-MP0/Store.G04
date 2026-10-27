using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.G04.Apis.Error;
using Store.G04.Apis.MiddleWares;
using Store.G04.Core;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.Services.Contract;
using Store.G04.Repository;
using Store.G04.Repository.Data;
using Store.G04.Repository.Data.Context;
using Store.G04.Service.Services.Products;
namespace Store.G04.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection"));
            });

            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new ProductProfile(builder.Configuration));
            });
            // to deals with the model validation error and return a custom response instead of the default 400 Bad Request response
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                 var Errors = actionContext.ModelState.Where(p =>p.Value.Errors.Count()>0)
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

            var app = builder.Build();

            app.UseMiddleware<Exceptionmiddleware>();

            // Database Migration and Seeding
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context =
                    services.GetRequiredService<StoreDbContext>();

                var loggerFactory =
                    services.GetRequiredService<ILoggerFactory>();

                try
                {
                    await context.Database.MigrateAsync();
                    await StoreDbContextSeed.SeedAsync(context);
                }
                catch (Exception ex)
                {
                    var logger =
                        loggerFactory.CreateLogger<Program>();

                    logger.LogError(ex, "Migration error!");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}"); // to handle the status code pages and redirect to the error controller 
            app.UseStaticFiles();   // Enable serving static files from wwwroot folder //  to show images in swagger 
        
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => "Welcome to Store API!");

            app.Run();
        }
    }


}
