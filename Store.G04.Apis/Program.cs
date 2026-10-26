using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.G04.Core;
using Store.G04.Core.Services.Contract;
using Store.G04.Repository;
using Store.G04.Repository.Data;
using Store.G04.Repository.Data.Context;
using Store.G04.Service.Services.Products;
using Store.G04.Core.Mapping.Products;
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
                cfg.AddProfile<ProductProfile>();
            });
            var app = builder.Build();

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

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => "Welcome to Store API!");

            app.Run();
        }
    }
}
