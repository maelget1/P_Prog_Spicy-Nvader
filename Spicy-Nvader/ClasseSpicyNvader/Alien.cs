using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    internal class Alien : Entity
    {
        private Timer timer;

        private int numberAlienX;

        private int numberAlienY;

        public int NumberAlienY { get => numberAlienY; set => numberAlienY = value; }
        public int NumberAlienX { get => numberAlienX; set => numberAlienX = value; }

        public Alien(int positionX, int positionY)
        {
            PositionX = positionX;

            PositionY = positionY;

            Skin = @"     ▀▄   ▄▀    ¦    ▄█▀███▀█▄    ¦   █▀███████▀█   ¦   █ █▀▀▀▀▀█ █   ¦      ▀▀ ▀▀      ";

            Width = 17;

            Height = 7;
        }

        public void Attack()
        {
            Laser laser = new Laser(PositionX, PositionY);
        }

        public void Move(object state)
        {
            if (PositionX + NumberAlienX < 330 && NumberAlienY % 2 == 1)
            {
                if(PositionX + NumberAlienX == 330)
                {
                    MoveDown();
                }
                else
                {
                    MoveRight();
                }
            }
            if(NumberAlienY % 2 == 0)
            {
                if(PositionX == 0)
                {
                    MoveDown();
                }
                else if(PositionX + NumberAlienX < 330)
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
    }
}
