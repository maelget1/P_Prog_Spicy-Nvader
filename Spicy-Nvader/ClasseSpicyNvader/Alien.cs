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
            if(PositionY < 300)
            {
                Console.MoveBufferArea(PositionX, PositionY, Width, Height, ++PositionX, PositionY);
            }
            else
            {

            }
            
        }
    }
}
