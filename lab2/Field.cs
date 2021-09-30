using System.Collections.Generic;

namespace lab2
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

        public bool IsFree(Coord coord)
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