﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using lab2;

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
                    services.AddSingleton<IWormLogic, WormLogicNearFood>();
                    services.AddScoped<IFoodGeneratorService, FoodGeneratorService>();
                    services.AddScoped<IWormLogicService, WormLogicService>();
                    services.AddScoped<IPrintService, PrintService>();
                    services.AddScoped<IGenerateNameService, GenerateNameService>();
                    services.AddHostedService<WorldSimulatorService>();
                });
        }

    } 
}
    
    