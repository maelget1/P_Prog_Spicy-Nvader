using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    internal class Wall : Entity
    {
        public Wall(int positionX, int positionY, byte life)
        {
            PositionX = positionX;

            PositionY = positionY;

            Life = life;
        }

        public void TakeDamage()
        {
            Life -= 1;
        }
    }
}
