
using Azure.Data.Tables;
using Azure.Storage.Blobs;

using Microsoft.EntityFrameworkCore;
using ItemsAPI.Data;

using ItemsAPI.Configuration;
using ItemsAPI.Services;

namespace ItemsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("AzureSql")));

            builder.Services.Configure<AzureTranslatorOptions>(
                builder.Configuration.GetSection("AzureTranslator"));

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<ITranslatorService, TranslatorService>();

            // Отримуємо рядок підключення з appsettings.json
            //string? connectionString =
            //    builder.Configuration["AzureStorage:ConnectionString"];

            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    throw new Exception(
            //        "Azure Storage Connection String не знайдено!");
            //}

            // Реєстрація Azure Blob Storage
            //builder.Services.AddSingleton(
            //    new BlobServiceClient(connectionString));

            //// Реєстрація Azure Table Storage
            //builder.Services.AddSingleton(
            //    new TableServiceClient(connectionString));

            // Стандартні сервіси ASP.NET Core
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}