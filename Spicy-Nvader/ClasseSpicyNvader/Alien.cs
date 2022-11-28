using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Alien : Entity
    {
        private int bigX;

        private int bigY;

        private int minY;

        private int minX;

        public int BigY { get => bigY; set => bigY = value; }
        public int BigX { get => bigX; set => bigX = value; }
        public int MinX { get => minX; set => minX = value; }
        public int MinY { get => minY; set => minY = value; }

        public Alien(int positionX, int positionY)
        {
            PositionX = positionX;

            PositionY = positionY;

            Skin = @"     ▀▄   ▄▀    ¦    ▄█▀███▀█▄    ¦   █▀███████▀█   ¦   █ █▀▀▀▀▀█ █   ¦      ▀▀ ▀▀      ";

            Width = Skin.Split("¦")[0].Length;

            Height = Skin.Split("¦").Length;
        }

        public Laser Attack()
        {
            Laser laser = new Laser(PositionX + 9, PositionY + 5);
            return laser;
        }

        public void MoveRight()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, ++PositionX, PositionY);
        }

        public void MoveLeft()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, --PositionX, PositionY);
        }

        public void MoveDown()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, PositionX, ++PositionY);
        }

        public void Erase()
        {
            Console.MoveBufferArea(0, 52, Width, Height, PositionX, PositionY);
        }
    }
}
