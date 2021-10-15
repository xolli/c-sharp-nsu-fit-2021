using lab3;

namespace lab4
{
    public class WormLogicMoveRight : IWormLogic
    {
        public WormAction MakeMove(Field field, Worm worm)
        {
            return WormAction.MoveRight;
        }
    }
}