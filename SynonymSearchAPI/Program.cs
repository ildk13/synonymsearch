using System.Diagnostics.CodeAnalysis;
using SynonymSearchAPI.Data;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddScoped<ISynonymService, SynonymService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseCors("AllowAll");

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}