﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasseSpicyNvader
{
    public class Wall : Entity
    {
        public Wall(int x, int y)
        {
            PositionX = x;

            PositionY = y;

            Life = 3;

            Skin = @"████████████████████¦████████████████████";

            Width = Skin.Split("¦")[0].Length;

            Height = Skin.Split("¦").Length;

        }

        public string Color { get => Color; set => Color = value; }

        public void TakeDamage(List<Wall> list)
        {
            Life--;
            if(Life == 0)
            {
                list.Remove(this);
                Erase();
            }
        }

        public void Erase()
        {
            Console.MoveBufferArea(0, 52, Width, Height, PositionX, PositionY);
        }
    }
}
