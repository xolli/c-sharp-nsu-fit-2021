namespace lab3
{
    public class Food
    {
        public Coord Coord { get; }

        private int _condition = 10;

        public Food(int x, int y)
        {
            Coord = new Coord(x, y);
        }

        public Food(Coord coord)
        {
            Coord = coord;
        }

        public bool IsSpoiled()
        {
            return _condition == 0;
        }

        public void Touch()
        {
            --_condition;
        }

        public override string ToString()
        {
            return "(" + Coord.X + ", " + Coord.Y + ")";
        }
        
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            var target = (Food)obj;
            return Coord.Equals(target.Coord) && _condition == target._condition;
        }

        public static bool operator== (Food p1, Food p2)
        {
            if (p1 is null)
            {
                if (p2 is null)
                {
                    return true;
                }
                return false;
            }
            return p1.Equals(p2);
        }
        
        public static bool operator!= (Food p1, Food p2)
        {
            return !(p1 == p2);
        }
    }
}