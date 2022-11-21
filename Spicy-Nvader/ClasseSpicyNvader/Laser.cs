using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Laser : Entity
    {
        private Timer timer;
        
        public Laser(int positionX, int positionY)
        {
            Width = 1;

            Height = 1;

            Skin = "|";

            PositionX = positionX;

            PositionY = positionY;

            Draw();
        }

        public void MoveAlien()
        {
            PositionY++;
        }

        public void MovePlayer()
        {
            if(PositionY > 1)
            {
                Console.MoveBufferArea(PositionX, PositionY, Width, Height, PositionX, --PositionY);
            }
            else if(PositionY == 1)
            {
                Erase();
            }
             
        }

        public void Erase()
        {
            Console.MoveBufferArea(0, 52, Width, Height, PositionX, PositionY);
        }
    }
}
