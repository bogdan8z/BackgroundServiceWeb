using BackgroundServiceWeb.Services.Interfaces;
using BackgroundServiceWeb.Services;
using BackgroundServiceWeb.Tasks;

namespace BackgroundServiceWeb
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
            MapInterfacesAndClasses(builder.Services);

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

        private static void MapInterfacesAndClasses(IServiceCollection services)
        {
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddHostedService<DoSomethingEveryXHoursBackgroundService>();
        }
    }
}