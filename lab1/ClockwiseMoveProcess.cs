using System;
using System.IO;
using System.Linq;

namespace lab1
{
    public class ClockwiseMoveProcess
    {
        private readonly Field _field;
        private readonly string _filename;
        private StreamWriter _file;

        public ClockwiseMoveProcess(string filename)
        {
            _filename = filename;
            _field = new Field();
            _field.Worms.Add(new Worm(0, 0, "John"));
        }
        public void Start()
        {
            using (_file = new StreamWriter(_filename))
            {
                for (int i = 0; i < 100; ++i)
                {
                    Move(i);
                    PrintWorms();
                }
                _file.Close();   
            }
        }

        private void FirstStep()
        {
            moveUp();
        }
        private void Move(int moveIndex)
        {
            if (moveIndex == 0)
            {
                FirstStep();
                return;
            }

            switch (moveIndex % 8)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveRight();
                    break;
                case 2:
                    MoveDown();
                    break;
                case 3:
                    MoveDown();
                    break;
                case 4:
                    MoveLeft();
                    break;
                case 5:
                    MoveLeft();
                    break;
                case 6:
                    moveUp();
                    break;
                case 7:
                    moveUp();
                    break;
            }
        }

        private void moveUp()
        {
            foreach (Worm worm in _field.Worms)
            {
                worm.MoveUp();
            }
        }
        
        private void MoveRight()
        {
            foreach (Worm worm in _field.Worms)
            {
                worm.MoveRight();
            }
        }
        
        private void MoveDown()
        {
            foreach (Worm worm in _field.Worms)
            {
                worm.MoveDown();
            }
        }
        
        private void MoveLeft()
        {
            foreach (Worm worm in _field.Worms)
            {
                worm.MoveLeft();
            }
        }
        
        private void PrintWorms()
        {
            Console.Write("Worms:[");
            foreach (Worm worm in _field.Worms)
            {
                Console.Write(worm.Name + '(' + worm.Coord.X + ',' + worm.Coord.Y + ')');
            }
            Console.WriteLine("]");
        }
    }
}