using lab2;
using lab3;

namespace lab4
{
    public class WormLogicIndividual : IWormLogic
    {
        public WormAction MakeMove(Field field, Worm worm)
        {
            switch (worm.Name.Substring(0, 2))
            {
                case "mu": return WormAction.MoveUp;
                case "ml": return WormAction.MoveLeft;
                case "md": return WormAction.MoveDown;
                case "mr": return WormAction.MoveRight;
                case "pu": return WormAction.ProduceUp;
                case "pl": return WormAction.ProduceLeft;
                case "pd": return WormAction.ProduceDown;
                case "pr": return WormAction.ProduceRight;
                default: return WormAction.Nothing;
            }
        }
    }
}