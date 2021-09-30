using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2
{
    public class NormalLifeProcess
    {
        private readonly Field _field;
        private static readonly Random RandomSource = new Random();

        public NormalLifeProcess()
        {
            _field = new Field();
            _field.Worms.Add(new Worm(0, 0, "John", 25));
        }

        public void Start()
        {
            for (int i = 0; i < 100; ++i)
            {
                Move();
                PrintField();
            }
        }

        private void PrintField()
        {
            Console.Write("Worms: [" + String.Join(", ", _field.Worms) + "], ");
            Console.WriteLine("Food: [" + String.Join(", ", _field.Foods) + "]");        
        }

        private void Move()
        {
            GenerateFood();
            TouchFood();
            ThrowOutSpoiled();
            Eat();
            MakeAction();
            Eat();
            KillWorms();
        }

        private void KillWorms()
        {
            List<Worm> deadWorms = new List<Worm>();
            foreach (var worm in _field.Worms)
            {
                if (worm.Health == 0)
                {
                    deadWorms.Add(worm);
                }
            }

            foreach (var deadWorm in deadWorms)
            {
                _field.Worms.Remove(deadWorm);
            }
        }

        private void MoveTo(Worm worm, Coord moveToCoord) {
            if (_field.IsFree(moveToCoord))
            {
                
            }
        }

        private void MakeAction()
        {
            List<Worm> newWorms = new List<Worm>();
            foreach (var worm in _field.Worms)
            {
                var action = WormLogic.MakeMove(_field, worm);
                switch (action)
                {
                    case WormAction.MoveUp:
                        if (_field.IsFree(new Coord(worm.Coord.X, worm.Coord.Y - 1)))
                        {
                            worm.Coord.Y -= 1;
                        }
                        break;
                    case WormAction.MoveLeft:
                        if (_field.IsFree(new Coord(worm.Coord.X - 1, worm.Coord.Y)))
                        {
                            worm.Coord.X -= 1;
                        }
                        break;
                    case WormAction.MoveDown:
                        if (_field.IsFree(new Coord(worm.Coord.X, worm.Coord.Y + 1)))
                        {
                            worm.Coord.Y += 1;
                        }
                        break;
                    case WormAction.MoveRight:
                        if (_field.IsFree(new Coord(worm.Coord.X + 1, worm.Coord.Y)))
                        {
                            worm.Coord.X += 1;
                        }
                        break;
                    case WormAction.ProduceUp:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X, worm.Coord.Y - 1)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X, worm.Coord.Y - 1, worm.Name + "_" + GenerateString(), 10));
                        }
                        break;
                    case WormAction.ProduceLeft:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X - 1, worm.Coord.Y)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X - 1, worm.Coord.Y, worm.Name + "_" + GenerateString(), 10));
                        }
                        break;
                    case WormAction.ProduceDown:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X, worm.Coord.Y + 1)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X, worm.Coord.Y + 1, worm.Name + "_" + GenerateString(), 10));
                        }
                        break;
                    case WormAction.ProduceRight:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X + 1, worm.Coord.Y)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X + 1, worm.Coord.Y, worm.Name + "_" + GenerateString(), 10));
                        }
                        break;
                    case WormAction.Nothing:
                        break;
                    default:
                        throw new Exception("Incorrect worm's action");
                }
                worm.Health -= 1;
            }

            foreach (var newWorm in newWorms)
            {
                _field.Worms.Add(newWorm);
            }
        }

        // https://jonathancrozier.com/blog/how-to-generate-a-random-string-with-c-sharp
        private String GenerateString(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RandomSource.Next(s.Length)]).ToArray());
            return randomString;
        }

        private void Eat()
        {
            foreach (var worm in _field.Worms)
            {
                Food food;
                if (_field.GetFood(worm.Coord, out food))
                {
                    worm.Health += 10;
                    _field.Foods.Remove(food);
                }
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

        private void TouchFood()
        {
            foreach (var food in _field.Foods)
            {
                food.Touch();
            }
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