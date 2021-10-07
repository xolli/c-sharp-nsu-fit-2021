using System;

namespace lab3
{
    public class Worm
    {
        public string Name { get; }
        public Coord Coord { get; }
        public int Health { get; set; }

        public Worm(int startX, int startY, String name, int health)
        {
            Coord = new Coord(startX, startY);
            Name = name;
            Health = health;
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

        public override string ToString()
        {
            return Name + '-' + Health + " (" + Coord.X + ',' + Coord.Y + ')';
        }
    }
}