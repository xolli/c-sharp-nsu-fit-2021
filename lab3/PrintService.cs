using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace lab3
{
    public class PrintService : IHostedService
    {
        private readonly Field _field;
        
        public PrintService(Field field)
        {
            _field = field;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.Write("Worms: [" + String.Join(", ", _field.Worms) + "], ");
            Console.WriteLine("Food: [" + String.Join(", ", _field.Foods) + "]");   
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}