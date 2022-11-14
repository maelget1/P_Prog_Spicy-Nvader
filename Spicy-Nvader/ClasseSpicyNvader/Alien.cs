using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Alien : Entity
    {
        private Timer timer;

        private int bigX;

        private int bigY;

        private int minY;

        private int minX;

        public int BigY { get => bigY; set => bigY = value; }
        public int BigX { get => bigX; set => bigX = value; }
        public int MinX { get => minX; set => minX = value; }
        public int MinY { get => minY; set => minY = value; }

        public Alien(int positionX, int positionY)
        {
            PositionX = positionX;

            PositionY = positionY;

            Skin = @"     ▀▄   ▄▀    ¦    ▄█▀███▀█▄    ¦   █▀███████▀█   ¦   █ █▀▀▀▀▀█ █   ¦      ▀▀ ▀▀      ";

            Width = 16;

            Height = 5;
        }

        public void Attack()
        {
            Laser laser = new Laser(PositionX, PositionY);
        }

        public void Move(object state)
        {
            if (MinX <= 300 - (Width * 6) && BigY % 2 == 0)
            {
                if (MinX == 300 - (Width * 6))
                {
                    MoveDown();
                }
                else
                {
                    MoveRight();
                }
            }
            if (BigX >= Width * 6 && BigY % 2 == 1)
            {
                if (MinX == 0)
                {
                    MoveDown();
                }
                else
                {
                    MoveLeft();
                }
            }
        }

        public void MoveRight()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, ++PositionX, PositionY);
        }

        public void MoveLeft()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, --PositionX, PositionY);
        }

        public void MoveDown()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, PositionX, ++PositionY);
        }

        public void Die()
        {

        }
    }
}
