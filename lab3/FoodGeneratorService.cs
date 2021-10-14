using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace lab3
{
    public class FoodGeneratorService : IFoodGeneratorService
    {
        private Field _field;
        private static readonly Random RandomSource = new Random();

        public FoodGeneratorService(Field field)
        {
            _field = field;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            GenerateFood();
            TouchFood();
            ThrowOutSpoiled();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        private void GenerateFood()
        {
            Coord newFoodCoord = new Coord(NextNormal(0, 5), NextNormal(0, 5));
            while (!CoordNoFood(newFoodCoord))
            {
                newFoodCoord = new Coord(NextNormal(0, 5), NextNormal(0, 5));
            }
            _field.Foods.Add(new Food(newFoodCoord));
        }
        
        private bool CoordNoFood(Coord coord)
        {
            foreach (Food food in _field.Foods)
            {
                if (food.Coord.X == coord.X && food.Coord.Y == coord.Y)
                {
                    return false;
                }
            }
            return true;
        }
        
        private void TouchFood()
        {
            foreach (var food in _field.Foods)
            {
                food.Touch();
            }
        }
        
        private void ThrowOutSpoiled()
        {
            List<Food> temp = new List<Food>();
            foreach (Food food in _field.Foods)
            {
                if (food.IsSpoiled())
                {
                    temp.Add(food);
                }
            }
            foreach (Food deletedFood in temp)
            {
                _field.Foods.Remove(deletedFood);
            }
        }
        
        private static int NextNormal(double mu = 0, double sigma = 1)
        {
            var u1 = RandomSource.NextDouble();
            var u2 = RandomSource.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mu + sigma * randStdNormal;
            return (int)Math.Round(randNormal);
        }
    }
}