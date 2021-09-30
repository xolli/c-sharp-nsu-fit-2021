namespace lab2
{
    public class Coord
    { 
        public Coord(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            var target = (Coord)obj;
            return (X == target.X) && (Y == target.Y);
        }
        
        public static bool operator== (Coord p1, Coord p2)
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
        
        public static bool operator!= (Coord p1, Coord p2)
        {
            return !(p1 == p2);
        }
    }
}