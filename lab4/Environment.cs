using lab3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lab4
{
    public class Environment
    {
        private readonly IHost _host;
        
        public Environment(IHostedService service)
        {
            _host = CreateHostBuilder(service).Build();
        }

        private IHostBuilder CreateHostBuilder(IHostedService service)
        {
            return Host.CreateDefaultBuilder(new string[] {}).ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<Field>();
                services.AddScoped<IHostedService, FoodGeneratorService>();
                services.AddScoped<IWormLogicService, WormLogicService>();
                services.AddScoped<IPrintService, PrintService>();
                services.AddScoped<IGenerateNameService, GenerateNameService>();
                services.AddHostedService<WorldSimulatorService>();
            });
        }

        public void Run()
        {
            _host.Run();
        }
    }
}