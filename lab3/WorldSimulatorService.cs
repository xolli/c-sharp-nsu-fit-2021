using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace lab3
{ public class WorldSimulatorService : BackgroundService
    {
        private readonly ILogger<WorldSimulatorService> _logger;
        private readonly IFoodGeneratorService _foodGeneratorService;
        private readonly IWormLogicService _wormLogicService;
        private readonly IPrintService _printService;
        private readonly Field _field;

        public WorldSimulatorService(IFoodGeneratorService foodGeneratorService, IWormLogicService wormLogicService,
            IPrintService printService, Field field, ILogger<WorldSimulatorService> logger)
        {
            _foodGeneratorService = foodGeneratorService;
            _wormLogicService = wormLogicService;
            _printService = printService;
            _field = field;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("World Simulator Service is running.");
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            _field.Worms.Add(new Worm(0, 0, "John", 25));
            int delayLoop = 0;
            while (!stoppingToken.IsCancellationRequested && delayLoop < 100)
            {
                await _foodGeneratorService.StartAsync(stoppingToken);
                await _wormLogicService.StartAsync(stoppingToken);
                await _printService.StartAsync(stoppingToken);
                ++delayLoop;
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("World Simulator Service is stopping.");
            await base.StopAsync(stoppingToken);
        }
    }
}


