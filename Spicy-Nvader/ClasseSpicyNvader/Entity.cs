using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Entity
    {
        private string skin;

        private int positionX;

        private int positionY;

        private byte life;

        public string Skin { get => skin; set => skin = value; }
        public int PositionX { get => positionX; set => positionX = value; }
        public int PositionY { get => positionY; set => positionY = value; }
        public byte Life { get => life; set => life = value; }

        public string draw()
        {
<<<<<<< HEAD
            skin.Split('¦');
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(Skin);
=======
            return skin;
>>>>>>> 594d5418a18e698d886166f3267795a7e9c9652c
        }
    }
}
