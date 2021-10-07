using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace lab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Field>();
                    services.AddScoped<FoodGeneratorService>();
                    services.AddScoped<WormLogicService>();
                    services.AddScoped<PrintService>();
                    services.AddScoped<GenerateNameService>();
                    services.AddHostedService<WorldSimulatorService>();
                });

        }

    } 
}
    
    