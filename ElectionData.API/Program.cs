using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElectionData.Data;
using ElectionData.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ElectionData.Data.Profiles;
using ElectionData.Data.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Configure the DbContext
        services.AddDbContext<ElectionDataDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        // Add Services
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IPollRepository, PollRepository>();
        services.AddScoped<IPollService, PollService>();

    })
    .Build();

host.Run();
