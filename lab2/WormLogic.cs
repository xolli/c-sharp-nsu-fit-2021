using System;
using System.Collections.Generic;

namespace lab2
{
    public enum WormAction
    {
        MoveUp,
        MoveLeft,
        MoveDown,
        MoveRight,
        ProduceUp,
        ProduceLeft,
        ProduceDown,
        ProduceRight,
        Nothing
    }

    public class WormLogic
    {
        private enum Direction
        {
            Up,
            Left,
            Down,
            Right,
            Nothing
        }

        // Finding the nearest food and going to it
        public static WormAction MakeMove(Field field, Worm worm)
        {
            Food nearestFood = FindNearestFood(worm, field.Foods);
            if (nearestFood == null)
            {
                return WormAction.Nothing;
            }
            if (worm.Health > 10 && CalcDistance(nearestFood.Coord, worm.Coord) <= 10)
            {
                return ProduceTo(worm.Coord, nearestFood.Coord, field);
            }
            if (worm.Health >= CalcDistance(nearestFood.Coord, worm.Coord))
            {
                return MoveTo(worm.Coord, nearestFood.Coord, field);
            }
            return WormAction.Nothing;
        }

        private static WormAction ProduceTo(Coord wormCoord, Coord nearestFoodCoord, Field field)
        {
            switch (FindFirstStep(wormCoord, nearestFoodCoord, field))
            {
                case Direction.Up:
                    return WormAction.ProduceUp;
                case Direction.Left:
                    return WormAction.ProduceLeft;
                case Direction.Down:
                    return WormAction.ProduceDown;
                case Direction.Right:
                    return WormAction.ProduceRight;
                default:
                    return WormAction.Nothing;
            }
        }

        private static WormAction MoveTo(Coord wormCoord, Coord nearestFoodCoord, Field field)
        {
            switch (FindFirstStep(wormCoord, nearestFoodCoord, field))
            {
                case Direction.Up:
                    return WormAction.MoveUp;
                case Direction.Left:
                    return WormAction.MoveLeft;
                case Direction.Down:
                    return WormAction.MoveDown;
                case Direction.Right:
                    return WormAction.MoveRight;
                default:
                    return WormAction.Nothing;
            }
        }


        // Calc the way from start to end and return first step
        private static Direction FindFirstStep(Coord start, Coord end, Field field)
        {
            Direction retDirection;
            FindShortestWay(start, end, field, out retDirection);
            return retDirection;
        }


        private static Food FindNearestFood(Worm worm, List<Food> foods)
        {
            if (foods.Count == 0)
            {
                return null;
            }

            int minDistance = CalcDistance(foods[0].Coord, worm.Coord);
            Food nearestFood = foods[0];
            foreach (var food in foods)
            {
                int tmp = CalcDistance(food.Coord, worm.Coord);
                if (tmp < minDistance)
                {
                    minDistance = tmp;
                    nearestFood = food;
                }
            }

            return nearestFood;
        }

        private static int CalcDistance(Coord a, Coord b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

        private static bool IsFreeWay(Field field, Coord start, Coord end, List<Direction> route)
        {
            Coord curPos = new Coord(start.X, start.Y);
            foreach (var move in route)
            {
                switch (move)
                {
                    case Direction.Up:
                        curPos.Y -= 1;
                        break;
                    case Direction.Left:
                        curPos.X -= 1;
                        break;
                    case Direction.Down:
                        curPos.Y += 1;
                        break;
                    case Direction.Right:
                        curPos.X += 1;
                        break;
                    default:
                        throw new Exception("Incorrect route");
                }

                if (!field.IsFree(curPos))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool FindShortestWay(Coord start, Coord end, Field field, out Direction direction)
        {
            int commonDistance = CalcDistance(start, end);
            List<Direction> route = new List<Direction>(commonDistance);
            Direction xDirection = Direction.Right;
            if (start.X > end.X)
            {
                xDirection = Direction.Left;
            }

            for (int i = 0; i < Math.Abs(start.X - end.X); ++i)
            {
                route.Add(xDirection);
            }

            Direction yDirection = Direction.Down;
            if (start.Y > end.Y)
            {
                yDirection = Direction.Up;
            }

            for (int i = 0; i < Math.Abs(start.Y - end.Y); ++i)
            {
                route.Add(yDirection);
            }

            bool lastPermutation = false;
            while (!IsFreeWay(field, start, end, route))
            {
                if (lastPermutation)
                {
                    Console.WriteLine("This is a very strange =/");
                    direction = Direction.Nothing;
                    return false;
                }
                lastPermutation = !NextRoute(route);
            }

            direction = route[0];
            return true;
        }

        // https://stackoverflow.com/questions/2390954/how-would-you-calculate-all-possible-permutations-of-0-through-n-iteratively/12768718#12768718
        private static bool NextPermutation<T>(IList<T> a) where T : IComparable
        {
            if (a.Count < 2) return false;
            var k = a.Count - 2;

            while (k >= 0 && a[k].CompareTo(a[k + 1]) >= 0) k--;
            if (k < 0) return false;

            var l = a.Count - 1;
            while (l > k && a[l].CompareTo(a[k]) <= 0) l--;

            var tmp = a[k];
            a[k] = a[l];
            a[l] = tmp;

            var i = k + 1;
            var j = a.Count - 1;
            while (i < j)
            {
                tmp = a[i];
                a[i] = a[j];
                a[j] = tmp;
                i++;
                j--;
            }

            return true;
        }

        private static List<int> RouteToNumbers(List<Direction> route)
        {
            List<int> retList = new List<int>(route.Count);
            for (int i = 0; i < route.Count; ++i)
            {
                switch (route[i])
                {
                    case Direction.Up:
                        retList.Add(2);
                        break;
                    case Direction.Left:
                        retList.Add(0);
                        break;
                    case Direction.Down:
                        retList.Add(3);
                        break;
                    case Direction.Right:
                        retList.Add(1);
                        break;
                    default:
                        throw new Exception("Incorrect route");
                }
            }
            return retList;
        }
        
        private static void NumbersToRoute(List<int> routeNumbers, List<Direction> route)
        {
            if (routeNumbers.Count != route.Count)
            {
                throw new Exception("Incorrect size of list routeNumbers or route");
            }
            for (int i = 0; i < routeNumbers.Count; ++i)
            {
                switch (routeNumbers[i])
                {
                    case 0:
                        route[i] = Direction.Left;
                        break;
                    case 1:
                        route[i] = Direction.Right;
                        break;
                    case 2:
                        route[i] = Direction.Up;
                        break;
                    case 3:
                        route[i] = Direction.Down;
                        break;
                    default:
                        throw new Exception("Incorrect route");
                }
            }
        }

        private static bool NextRoute(List<Direction> route)
        {
            var routeNumb = RouteToNumbers(route);
            if (!NextPermutation(routeNumb))
            {
                return false;
            }
            NumbersToRoute(routeNumb, route);
            return true;
        }
    }
}