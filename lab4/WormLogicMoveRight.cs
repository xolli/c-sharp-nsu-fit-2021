using lab3;

namespace lab4
{
    public class WormLogicMoveRight : WormLogic
    {
        public override WormAction MakeMove(Field field, Worm worm)
        {
            return WormAction.MoveRight;
        }
    }
}