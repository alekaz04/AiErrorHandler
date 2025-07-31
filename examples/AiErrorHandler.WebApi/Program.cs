using AiErrorHandler.AspNetCore;
using AiErrorHandler.AspNetCore.Extensions;
using AiErrorHandler.Models;

namespace AiErrorHandler.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAiErrorHandler(configure =>
        {
            configure.Model = "gpt-4o-2024-05-13";
            configure.Key = "";
            configure.IsWriteToLogger = true;
            configure.ProcessInBackground = true;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseAiErrorHandlerMiddleware();


        app.MapControllers();

        app.Run();
    }
}
