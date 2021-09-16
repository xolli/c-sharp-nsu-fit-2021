using System;

namespace lab1
{
    public class Worm
    {
        public string Name { get; }
        public Coord Coord { get; }

        public Worm(int startX, int startY, String name)
        {
            Coord = new Coord(startX, startY);
            Name = name;
        }

        public void MoveUp()
        {
            Coord.Y -= 1;
        }

        public void MoveRight()
        {
            Coord.X += 1;
        }

        public void MoveDown()
        {
            Coord.Y += 1;
        }

        public void MoveLeft()
        {
            Coord.X -= 1;
        }
    }
}