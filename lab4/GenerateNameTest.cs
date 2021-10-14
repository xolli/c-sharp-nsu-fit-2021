using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using lab3;

namespace lab4
{
    [TestFixture]
    public class GenerateNameTest
    {
        [Test]
        public void TestUniqueness()
        {
            GenerateNameService service = new GenerateNameService();
            Task<String> taskService1 = (Task<String>) service.StartAsync(new CancellationToken(false));
            Task<String> taskService2 = (Task<String>) service.StartAsync(new CancellationToken(false));
            Assert.AreNotEqual(taskService1.GetAwaiter().GetResult(), taskService2.GetAwaiter().GetResult());
        }
    }
}