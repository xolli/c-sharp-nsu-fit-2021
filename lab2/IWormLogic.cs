using lab2;

namespace lab3
{
    public interface IWormLogic
    {
        WormAction MakeMove(Field field, Worm worm);
    }
}