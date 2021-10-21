using lab2;
using lab3;
using System.Threading;
using NUnit.Framework;

namespace lab4
{
    [TestFixture]
    public class WormLogicTest
    {
        [Test]
        public void GenerateFoodWhereWorm()
        {
            Field field = new Field();
            Coord wormCoord = new Coord(0, 0);
            field.Worms.Add(new Worm(wormCoord.X, wormCoord.Y, "testName", 999));
            field.Foods.Add(new Food(wormCoord));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicNearFood());
            service.StartAsync(new CancellationToken(false));
            Assert.Zero(field.Foods.Count);
            Assert.AreEqual(field.Worms[0].Health, 999 - 1 + 10);
        }

        [Test]
        public void MovingNearFood()
        {
            Field field = new Field();
            field.Worms.Add(new Worm(0, 0, "testName", 999));
            field.Foods.Add(new Food(0, 10));
            field.Foods.Add(new Food(0, -20));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicNearFood());
            for (int i = 0; i < 10; ++i)
            {
                service.StartAsync(new CancellationToken(false));
            }
            Assert.AreEqual( field.Foods.Count, 1);
            Assert.AreEqual(field.Foods[0].Coord, new Coord(0, -20));
        }

        [Test]
        public void MovingToEmptyCell()
        {
            Field field = new Field();
            field.Worms.Add(new Worm(0, 0, "testName", 999));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicMoveRight());
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Worms.Count, 1);
            Assert.AreEqual(field.Worms[0].Coord, new Coord(1, 0));
        }
        
        [Test]
        public void MovingToCellWithEat()
        {
            Field field = new Field();
            int startHealth = 999;
            field.Worms.Add(new Worm(0, 0, "testName", startHealth));
            field.Foods.Add(new Food(1, 0));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicMoveRight());
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Worms.Count, 1);
            Assert.AreEqual(field.Worms[0].Health, startHealth - 1 + 10);
        }
        
        [Test]
        public void MovingToCellWithOtherWorm()
        {
            Field field = new Field();
            int startHealth = 999;
            field.Worms.Add(new Worm(0, 0, "mr", startHealth));
            field.Worms.Add(new Worm(1, 0, "nothing", startHealth));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicIndividual());
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Worms.Count, 2);
            Assert.AreEqual(field.Worms[0].Coord, new Coord(0, 0));
            Assert.AreEqual(field.Worms[1].Coord, new Coord(1, 0));
            Assert.AreEqual(field.Worms[0].Health, startHealth - 1);
        }
        
        [Test]
        public void ProduceSuccess()
        {
            Field field = new Field();
            int startHealth = 999;
            field.Worms.Add(new Worm(0, 0, "pr", startHealth));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicIndividual());
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Worms.Count, 2);
            Assert.AreEqual(field.Worms[0].Coord, new Coord(0, 0));
            Assert.AreEqual(field.Worms[1].Coord, new Coord(1, 0));
            Assert.AreEqual(field.Worms[0].Health, startHealth - 11);
            Assert.AreEqual(field.Worms[1].Health, 10);
        }
        
        [Test]
        public void ProduceFailOtherWorm()
        {
            Field field = new Field();
            int startHealth = 999;
            field.Worms.Add(new Worm(0, 0, "pr", startHealth));
            field.Worms.Add(new Worm(1, 0, "nothing", startHealth));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicIndividual());
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Worms.Count, 2);
            Assert.AreEqual(field.Worms[0].Coord, new Coord(0, 0));
            Assert.AreEqual(field.Worms[1].Coord, new Coord(1, 0));
            Assert.AreEqual(field.Worms[0].Health, startHealth - 1);
        }
        
        [Test]
        public void ProduceFailBusyFood()
        {
            Field field = new Field();
            int startHealth = 999;
            field.Worms.Add(new Worm(0, 0, "pr", startHealth));
            field.Foods.Add(new Food(1, 0));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicIndividual());
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Worms.Count, 1);
            Assert.AreEqual(field.Worms[0].Coord, new Coord(0, 0));
            Assert.AreEqual(field.Worms[0].Health, startHealth - 1);
        }
        
        [Test]
        public void ProduceFailLittleHealth()
        {
            Field field = new Field();
            int startHealth = 10;
            field.Worms.Add(new Worm(0, 0, "pr", startHealth));
            WormLogicService service = new WormLogicService(field, new GenerateNameService(), new WormLogicIndividual());
            service.StartAsync(new CancellationToken(false));
            Assert.AreEqual(field.Worms.Count, 1);
            Assert.AreEqual(field.Worms[0].Coord, new Coord(0, 0));
            Assert.AreEqual(field.Worms[0].Health, startHealth - 1);
        }
    }
}
