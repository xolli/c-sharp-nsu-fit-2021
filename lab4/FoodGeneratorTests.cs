using System.Threading;
using lab3;
using NUnit.Framework;
using lab2;

namespace lab4
{
    [TestFixture]
    public class FoodGeneratorTests
    {
        [Test]
        public void TestUniqueness()
        {
            Field field = new Field();
            FoodGeneratorService service = new FoodGeneratorService(field);
            service.StartAsync(new CancellationToken(false));
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Foods.Count, 2);
            Assert.AreNotEqual(field.Foods[0].Coord, field.Foods[1].Coord);
        }
    }
}