using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace lab3
{
    public class WormLogicService : IWormLogicService
    {
        
        private readonly Field _field;
        private readonly IGenerateNameService _generateNameService;
        private static readonly Random RandomSource = new Random();
        private static WormLogic _wormLogic;

        public WormLogicService(Field field, IGenerateNameService generateNameService, WormLogic wormLogic)
        {
            _field = field;
            _generateNameService = generateNameService;
            _wormLogic = wormLogic;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Eat();
            MakeAction(cancellationToken);
            Eat();
            KillWorms();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
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
        
        private void MakeAction(CancellationToken cancellationToken)
        {
            List<Worm> newWorms = new List<Worm>();
            foreach (var worm in _field.Worms)
            {
                var action = _wormLogic.MakeMove(_field, worm);
                switch (action)
                {
                    case WormAction.MoveUp:
                        if (_field.NoWorm(new Coord(worm.Coord.X, worm.Coord.Y - 1)))
                        {
                            worm.Coord.Y -= 1;
                        }
                        break;
                    case WormAction.MoveLeft:
                        if (_field.NoWorm(new Coord(worm.Coord.X - 1, worm.Coord.Y)))
                        {
                            worm.Coord.X -= 1;
                        }
                        break;
                    case WormAction.MoveDown:
                        if (_field.NoWorm(new Coord(worm.Coord.X, worm.Coord.Y + 1)))
                        {
                            worm.Coord.Y += 1;
                        }
                        break;
                    case WormAction.MoveRight:
                        if (_field.NoWorm(new Coord(worm.Coord.X + 1, worm.Coord.Y)))
                        {
                            worm.Coord.X += 1;
                        }
                        break;
                    case WormAction.ProduceUp:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X, worm.Coord.Y - 1)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X, worm.Coord.Y - 1, GenerateWormName(cancellationToken), 10));
                        }
                        break;
                    case WormAction.ProduceLeft:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X - 1, worm.Coord.Y)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X - 1, worm.Coord.Y, GenerateWormName(cancellationToken), 10));
                        }
                        break;
                    case WormAction.ProduceDown:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X, worm.Coord.Y + 1)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X, worm.Coord.Y + 1, GenerateWormName(cancellationToken), 10));
                        }
                        break;
                    case WormAction.ProduceRight:
                        if (worm.Health > 10 && _field.IsFree(new Coord(worm.Coord.X + 1, worm.Coord.Y)))
                        {
                            worm.Health -= 10;
                            newWorms.Add(new Worm(worm.Coord.X + 1, worm.Coord.Y, GenerateWormName(cancellationToken), 10));
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
        private String GenerateWormName(CancellationToken cancellationToken)
        {
            Task<String> generatedName = (Task<string>) _generateNameService.StartAsync(cancellationToken);
            return generatedName.GetAwaiter().GetResult();
        }
    }
}