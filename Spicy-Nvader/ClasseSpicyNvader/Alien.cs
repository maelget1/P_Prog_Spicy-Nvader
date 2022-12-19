using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Alien : Entity
    {
        /// <summary>
        /// constructeur de la classe
        /// </summary>
        public Alien(int positionX, int positionY)
        {
            //style de l'entitée
            Skin = @"     ▀▄   ▄▀    ¦    ▄█▀███▀█▄    ¦   █▀███████▀█   ¦   █ █▀▀▀▀▀█ █   ¦      ▀▀ ▀▀      ";

            //largeur de l'entitée
            Width = Skin.Split("¦")[0].Length;

            //hauteur de l'entitée
            Height = Skin.Split("¦").Length;

            //positionX de l'entitée
            PositionX = positionX * Width + 5;

            //positionY de l'entitée
            PositionY = positionY * Height + 1;
        }

        /// <summary>
        /// attaque en lançant un laser
        /// </summary>
        /// <returns></returns>
        public Laser Attack()
        {
            Laser laser = new Laser(PositionX + 9, PositionY + 5);
            return laser;
        }

        /// <summary>
        /// permet de se déplacer à droite
        /// </summary>
        public void MoveRight()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, ++PositionX, PositionY);
        }

        /// <summary>
        /// permet de se déplacer à gauche
        /// </summary>
        public void MoveLeft()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, --PositionX, PositionY);
        }

        /// <summary>
        /// permet de se déplacer vers le bas
        /// </summary>
        public void MoveDown()
        {
            Console.MoveBufferArea(PositionX, PositionY, Width, Height, PositionX, ++PositionY);
        }
    }
}
