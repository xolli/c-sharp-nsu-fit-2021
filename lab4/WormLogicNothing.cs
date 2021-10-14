using lab3;

namespace lab4
{
    public class WormLogicNothing : WormLogic
    {
        public override WormAction MakeMove(Field field, Worm worm)
        {
            return WormAction.Nothing;
        }
    }
}