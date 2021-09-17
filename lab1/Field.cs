using System.Collections.Generic;

namespace lab1
{
    public class Field
    {
        public List<Worm> Worms { get; }

        public Field()
        {
            Worms = new List<Worm>();
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
    }
}