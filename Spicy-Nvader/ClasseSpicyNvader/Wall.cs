using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    internal class Wall : Entity
    {
        public Wall()
        {
            PositionX = 30;

            PositionY = 50;

            Life = 3;

            Skin = @"████████████████████¦████████████████████";

            Width = 20;

            Height = 2;
        }

        public void TakeDamage()
        {
            Life -= 1;
        }
    }
}
