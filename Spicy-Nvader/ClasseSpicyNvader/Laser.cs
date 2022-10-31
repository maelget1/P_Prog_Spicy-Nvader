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
            timer = new Timer(new TimerCallback(MovePlayer));

            Width = 1;

            Height = 1;

            Skin = "|";

            timer.Change(0, 50);

            PositionX = positionX;

            PositionY = positionY;

            Draw();
        }

        public void MoveAlien()
        {
            PositionY++;
        }

        private void MovePlayer(object state)
        {
            if(PositionY > 0)
            {
                Console.MoveBufferArea(PositionX, PositionY, Width, Height, PositionX, --PositionY);
            }
            else
            {
                
            }
             
        }
    }
}
