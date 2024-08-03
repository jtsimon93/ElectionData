using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ElectionData.Data;
using ElectionData.Data.Repositories;
using ElectionData.Data.Services;
using ElectionData.Scraper.Services;
using AutoMapper;

namespace ElectionData.Scraper
{
    class Program
    {
        public static IConfiguration Configuration { get; private set; }

        static void Main(string[] args)
        {
            // Build configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.appSettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            // Set up dependency injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Apply DB migrations
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ElectionDataDbContext>();
                dbContext.Database.Migrate();
                Console.WriteLine("Migrations applied.");
            }

            // Run the app
            var app = serviceProvider.GetService<App>();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Register configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Register DbContext
            services.AddDbContext<ElectionDataDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Mapper
            services.AddAutoMapper(typeof(Profile));

            // Register services
            services.AddSingleton<App>();
            services.AddScoped<IPollRepository, PollRepository>();
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IPollProcessorService, PollProcessorService>();
            services.AddScoped<IPollCleanerService, PollCleanerService>();
            services.AddScoped<RealClearPollingCollectorService>();

            // Factory for creating IPollService instances
            services.AddSingleton<Func<string, IPollCollectorService>>(services => key =>
            {
                switch (key)
                {
                    case "RealClear":
                        return services.GetService<RealClearPollingCollectorService>();
                    default:
                        throw new ArgumentException("Invalid service key", nameof(key));
                }
            });
        }
    }
}
