using Api_BigchainDb.Queue;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api_BigchainDb
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logBuilder =>
            {
                logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                logBuilder.AddConsole();
                logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<MonitorLoop>();
                services.AddHostedService<QueuedHostedService>();
                services.AddSingleton<IBackgroundTaskQueue>(_ =>
                {
                    if (!int.TryParse(context.Configuration["QueueCapacity"], out var queueCapacity))
                    {
                        queueCapacity = 100;
                    }
                    var dbt = new DefaultBackgroundTaskQueue(queueCapacity);
                    return dbt;
                });
            });
    }
}
