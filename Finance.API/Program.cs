using Finance.Domain.Models.Configs;
using Finance.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Finance.Service.Extensions;

namespace Finance.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<EncryptionConfigs>(builder.Configuration.GetSection("EncryptionKey"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<EncryptionConfigs>>().Value);

            builder.Services.AddDbContext<FinanceContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var configuration = builder.Configuration;

            builder.Services.AddServices(configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
