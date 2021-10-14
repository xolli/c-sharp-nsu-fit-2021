using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace lab3
{
    public class GenerateNameService : IGenerateNameService
    {
        private static readonly Random RandomSource = new Random();
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return GetNextNameTask();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private Task<String> GetNextNameTask()
        {
            return Task.Run( () => "Worm_" + GenerateString());
        }
        
        // https://jonathancrozier.com/blog/how-to-generate-a-random-string-with-c-sharp
        private String GenerateString(int length = 4)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomSource.Next(s.Length)]).ToArray());
            return randomString;
        }
    }
}