using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Laser : Entity
    {
        public Laser(int positionX, int positionY)
        {
            Skin = "|";

            PositionX = positionX;

            PositionY = positionY;
        }

        public void MoveAlien()
        {
            PositionY++;
        }

        public void MovePlayer()
        {
            while(PositionY > 0)
            {
                Draw();
                Thread.Sleep(50);
                PositionY--;
                Console.SetCursorPosition(PositionX, PositionY + 1);
                Console.Write(" ");
                
            }
            
        }
    }
}
