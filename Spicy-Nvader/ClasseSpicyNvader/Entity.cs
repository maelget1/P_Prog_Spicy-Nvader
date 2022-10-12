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

        public string Draw()
        {
            int compteur = 0;
            string[] subs = skin.Split('¦');
            foreach(string s in subs)
            {
                Console.SetCursorPosition(PositionX, PositionY+compteur);
                Console.Write(s);
                compteur++;
            }
            return skin;
        }
    }
}
