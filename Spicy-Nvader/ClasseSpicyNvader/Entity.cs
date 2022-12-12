using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Entity
    {
        private string _skin;

        private int _positionX;

        private int _positionY;

        private byte _life;

        private int _width;

        private int _height;

        public string Skin { get => _skin; set => _skin = value; }
        public int PositionX { get => _positionX; set => _positionX = value; }
        public int PositionY { get => _positionY; set => _positionY = value; }
        public byte Life { get => _life; set => _life = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }

        public string Draw()
        {
            int compteur = 0;
            string[] subs = _skin.Split('¦');
            foreach(string s in subs)
            {
                Console.SetCursorPosition(_positionX, _positionY + compteur);
                Console.Write(s);
                compteur++;
            }
            return _skin;
        }

        public void Erase()
        {
            Console.MoveBufferArea(0, 52, _width, _height, _positionX, _positionY);
        }
    }
}
