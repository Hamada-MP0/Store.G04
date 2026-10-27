using Microsoft.EntityFrameworkCore;
using Store.G04.Apis.MiddleWares;
using Store.G04.Repository.Data;
using Store.G04.Repository.Data.Context;

namespace Store.G04.Apis.Helper
{
    public static class ConfigiureMiddleWare
    {
        public static async Task<WebApplication> ConfigureMiddleWareAsync(this WebApplication app)
        {
            

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

            // Configure the HTTP request pipeline.
            app.UseMiddleware<Exceptionmiddleware>();

            app.UseStatusCodePagesWithReExecute("/error/{0}"); // to handle the status code pages and redirect to the error controller 
            app.UseStaticFiles();   // Enable serving static files from wwwroot folder //  to show images in swagger 

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapGet("/", () => "Welcome to Store API!");
            return app;
        }
    }
}
