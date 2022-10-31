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

        private int width;

        private int height;

        public string Skin { get => skin; set => skin = value; }
        public int PositionX { get => positionX; set => positionX = value; }
        public int PositionY { get => positionY; set => positionY = value; }
        public byte Life { get => life; set => life = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public string Draw()
        {
            int compteur = 0;
            string[] subs = skin.Split('¦');
            foreach(string s in subs)
            {
                Console.SetCursorPosition(PositionX, PositionY + compteur);
                Console.Write(s);
                compteur++;
            }
            return skin;
        }

        public string DrawOnce(int x, int y)
        {
            int compteur = 0;
            string[] subs = skin.Split('¦');
            foreach (string s in subs)
            {
                Console.SetCursorPosition(x, y + compteur);
                Console.Write(s);
                compteur++;
            }
            return skin;
        }
    }
}
