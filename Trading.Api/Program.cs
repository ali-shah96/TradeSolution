
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Trading.Api.Helpers;
using Trading.Api.Repositories;
using Trading.Api.Repositories.Imp;
using TradingPublisher.Services.Imp;
using Trading.Api.Data;
using Trading.Api.Services;


namespace Trading.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add database context (EF Core) with SQL Server
            builder.Services.AddDbContext<TradeDbContext>(options =>
                options.UseSqlServer(@"Data Source=localhost\\SQLEXPRESS;Initial Catalog=TradeDb;Integrated Security=True;TrustServerCertificate=True;"));

            // Register MassTransit with RabbitMQ
            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });

            // Register application services and repositories
            builder.Services.AddScoped<ITradeRepository, TradeRepository>();
            builder.Services.AddScoped<ITradeService, TradeService>();

            // Add AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // Add controllers
            builder.Services.AddControllers();

            // Enable API Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "Trading API", Version = "v1" });
            });

            #endregion

            var app = builder.Build();

            #region Configure Middleware

            // Enable Swagger in development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trading API v1");
                });
            }

            // Global exception handling
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var result = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            error = error.Error.Message
                        });
                        await context.Response.WriteAsync(result);
                    }
                });
            });

            // Redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // Map controller endpoints
            app.MapControllers();

            #endregion

            app.Run();

        }
    }
}
