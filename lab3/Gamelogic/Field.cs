using System;
using System.Collections.Generic;

namespace lab3
{
    public class Field
    {
        public List<Worm> Worms { get; }
        public List<Food> Foods { get; }

        public Field()
        {
            Worms = new List<Worm>();
            Foods = new List<Food>();
        }

        public bool NoWorm(Coord coord)
        {
            foreach (Worm worm in Worms)
            {
                if (worm.Coord.X == coord.X & worm.Coord.Y == coord.Y)
                {
                    return false;
                }
            }

            return true;
        }

        public bool NoFood(Coord coord)
        {
            foreach (Food food in Foods)
            {
                if (food.Coord.X == coord.X & food.Coord.Y == coord.Y)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsFree(Coord coord)
        {
            return NoFood(coord) && NoWorm(coord);
        }

        public bool GetFood(Coord coord, out Food food)
        {
            foreach (var f in Foods)
            {
                if (f.Coord == coord)
                {
                    food = f;
                    return true;
                }
            }

            food = null;
            return false;
        }
    }
}