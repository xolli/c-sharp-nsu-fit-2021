using System;
using System.IO;

namespace lab1
{
    public class ClockwiseMoveProcess
    {
        private Field _field;
        private string _filename;
        private StreamWriter _file;

        public ClockwiseMoveProcess(string filename)
        {
            _filename = filename;
            _field = new Field();
            _field.Worms.Add(new Worm(0, 0, "John"));
        }
        public void Start()
        {
            _file = new StreamWriter(_filename);
            for (int i = 0; i < 100; ++i)
            {
                Move(i);
                PrintWorms();
            }
            _file.Close();
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
                    moveRight();
                    break;
                case 1:
                    moveRight();
                    break;
                case 2:
                    moveDown();
                    break;
                case 3:
                    moveDown();
                    break;
                case 4:
                    moveLeft();
                    break;
                case 5:
                    moveLeft();
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
        
        private void moveRight()
        {
            foreach (Worm worm in _field.Worms)
            {
                worm.MoveRight();
            }
        }
        
        private void moveDown()
        {
            foreach (Worm worm in _field.Worms)
            {
                worm.MoveDown();
            }
        }
        
        private void moveLeft()
        {
            foreach (Worm worm in _field.Worms)
            {
                worm.MoveLeft();
            }
        }
        
        private void PrintWorms()
        {
            _file.Write("Worms:[");
            foreach (Worm worm in _field.Worms)
            {
                _file.Write(worm.Name + '(' + worm.Coord.X + ',' + worm.Coord.Y + ')');
            }
            _file.WriteLine("]");
        }
    }
}