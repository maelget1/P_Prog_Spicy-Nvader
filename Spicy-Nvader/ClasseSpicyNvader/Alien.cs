using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    internal class Alien : Entity
    {
        public Alien(int positionX, int positionY)
        {
            PositionX = positionX;

            PositionY = positionY;
        }

        public void Attack()
        {
            Laser laser = new Laser(PositionX, PositionY);
        }
    }
}
