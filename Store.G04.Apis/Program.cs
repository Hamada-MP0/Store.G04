using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.G04.Apis.Error;
using Store.G04.Apis.Helper;
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
           builder.Services.AddDependency(builder.Configuration);
            //  builder.Services.AddControllers();
            var app = builder.Build();
            // Configure the HTTP request pipeline. and configure the middleware for the application, such as exception handling, status code pages, static files, etc.
            await app.ConfigureMiddleWareAsync();







            app.Run();
        }
    }


}
