﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WMPLib;

namespace ClasseSpicyNvader
{
    public class Game
    {
        //////////////////////////////////////////////////variables//////////////////////////////////////////////////
        string name;
        int numberEnnemiesX;
        int numberEnnemiesY;
        int alienX;
        int alienY;
        int cursorBreak;
        bool quitOrStart;
        int randomAlien;
        bool playing;
        Player player;
        Random random = new Random();
        Menu menu = new Menu();
        Wall wall1 = new Wall(30, 50);
        Wall wall2 = new Wall(100, 50);
        Wall wall3 = new Wall(150, 50);
        List<Alien> ennemies = new List<Alien>();
        List<Laser> lasersPlayer = new List<Laser>();
        List<Wall> walls = new List<Wall>();
        List<Laser> lasersAlien = new List<Laser>();
        Timer timerAlien;
        Timer timerLaserPlayer;
        Timer timerLaserEnnemies;

        public void PlayGame()
        {
            menu.Playing = false;

            //efface la console
            Console.Clear();

            //place le curseur au milieu de l'écran
            Console.SetCursorPosition(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2);

            //demande un pseudo
            Console.Write("Quel est votre pseudo ? : ");

            //mets le pseudo dans le nom
            name = Console.ReadLine();

            //efface la console
            Console.Clear();

            //si le pseudo magique (Excalibreizh) est entrée alors le joueur à 5 vies
            if (name == "ExcaliBreizh")
            {
                //instancie un nouveau joueur avec le nom qu'il a entré
                player = new Player(name, 107, 56, 5);
            }

            //si le pseudo est autres
            else if(name == "Saul")
            {
                EasterEgg();

                //instancie un nouveau joueur avec le nom qu'il a entré
                player = new Player(name, 107, 56, 3);
            }

            else
            {
                //instancie un nouveau joueur avec le nom qu'il a entré
                player = new Player(name, 107, 56, 3);
            }

            playing = true;

            InitiateAlien();

            InitiateWall();

            Stats(player);

            timerAlien = new Timer(new TimerCallback(ActionEnemies));
            timerAlien.Change(0, 200);

            timerLaserPlayer = new Timer(new TimerCallback(LaserMovementPlayer));
            timerLaserPlayer.Change(0, 50);

            timerLaserEnnemies = new Timer(new TimerCallback(LaserMovementEnnemies));
            timerLaserEnnemies.Change(0, 50);

            GameMusic();

            player.Draw();

            //le fait tant que c'est pas fini
            do
            { 
                //lis les touches cliquées
                switch (Console.ReadKey(true).Key)
                {
                    //si flèche de droite
                    case ConsoleKey.RightArrow:

                        //appele la fonction qui permet d'aller à droite
                        player.GoRight();

                        //quitte l'action
                        break;

                    //si flèche de gauche
                    case ConsoleKey.LeftArrow:

                        //appelle la fonction pour aller à gauche
                        player.GoLeft();

                        //quitte l'action
                        break;

                    //si espace
                    case ConsoleKey.Spacebar:

                        //appelle la fonction pour tirer
                        lasersPlayer.Add(player.Attack());

                        //quitte l'action
                        break;

                    //si escape
                    case ConsoleKey.Escape:

                        Break();

                        //quitte l'action
                        break;

                    default:
                        break;
                }

               

            } while (playing);
        }

        private void ActionEnemies(object state)
        {
            int minX = ennemies.Min(elements => elements.PositionX);
            int bigX = ennemies.Max(elements => elements.PositionX);
            bigX = ennemies.Max(elements => elements.Width) + bigX;
            int minY = ennemies.Max(elements => elements.PositionY);
            int bigY = ennemies.Max(elements => elements.PositionY);
            bigY = ennemies.Max(elements => elements.Height) + bigY;
            foreach (Alien element in ennemies.ToArray())
            {
                randomAlien = random.Next(0, 10);
                element.Draw();
                if (minX <= 240 - (element.Width * 6) && bigY % 2 == 1)
                {
                    if (minX == 240 - (element.Width * 6))
                    {
                        element.MoveDown();
                    }
                    else
                    {
                        element.MoveRight();
                        if(element.PositionY + element.Height == bigY && randomAlien == 3)
                        {
                            lasersAlien.Add(element.Attack());
                        }
                    }
                }
                else if (bigX >=element.Width * 6 && bigY % 2 == 0)
                {
                    if (minX == 0)
                    {
                        element.MoveDown();
                    }
                    else
                    {
                        element.MoveLeft();
                        if (element.PositionY + element.Height == bigY && randomAlien == 3)
                        {
                            lasersAlien.Add(element.Attack());
                        }
                    }
                }
                foreach (Laser laser in lasersPlayer.ToArray())
                {
                    if (element.PositionX <= laser.PositionX && element.PositionX + element.Width >= laser.PositionX && element.PositionY <= laser.PositionY && element.PositionY + element.Height >= laser.PositionY)
                    {
                        KillAlien(laser, element);
                    }                
                }
                if(bigY == 50)
                {
                    GameOver();
                }
            }
        }

        private void LaserMovementPlayer(object state)
        {
            foreach(Laser laser in lasersPlayer.ToArray())
            {
                laser.MovePlayer();
                foreach(Wall wall in walls.ToArray())
                {
                    if (wall.PositionX <= laser.PositionX && wall.PositionX + wall.Width >= laser.PositionX && wall.PositionY <= laser.PositionY && wall.PositionY + wall.Height >= laser.PositionY)
                    {
                        lasersPlayer.Remove(laser);
                        laser.Erase();
                        wall.TakeDamage(walls);
                        if(wall.Life == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            wall.Draw();
                            Console.ResetColor();
                        }
                        if(wall.Life == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            wall.Draw();
                            Console.ResetColor();
                        }
                    }
                }
            }
        }

        private void LaserMovementEnnemies(object state)
        {
            foreach (Laser laser in lasersAlien.ToArray())
            {
                laser.MoveAlien();
                foreach (Wall wall in walls.ToArray())
                {
                    if (wall.PositionX <= laser.PositionX && wall.PositionX + wall.Width >= laser.PositionX && wall.PositionY <= laser.PositionY && wall.PositionY + wall.Height >= laser.PositionY)
                    {
                        lasersAlien.Remove(laser);
                        laser.Erase();
                        wall.TakeDamage(walls);
                        if (wall.Life == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            wall.Draw();
                            Console.ResetColor();
                        }
                        if (wall.Life == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            wall.Draw();
                            Console.ResetColor();
                        }
                    }   
                }
                if (laser.PositionX >= player.PositionX && laser.PositionX <= player.PositionX + player.Width && laser.PositionY >= player.PositionY && laser.PositionY <= player.PositionY + player.Height)
                {
                    lasersAlien.Remove(laser);
                    laser.Erase();
                    player.LoseLife();
                    Stats(player);
                    if(player.Life == 0)
                    {
                        GameOver();
                    }
                }  
            }
        }

        public void GameOver()
        {
            Console.Clear();

            playing = false;

            timerAlien.Dispose();

            timerLaserEnnemies.Dispose();

            timerLaserPlayer.Dispose();

            ennemies.Clear();

            lasersAlien.Clear();

            lasersPlayer.Clear();

            walls.Clear();

            menu.AddBestScore(player.Score);

            menu.Playing = true;

            Console.SetCursorPosition(Console.LargestWindowWidth / 2,0);

            Console.Write(@"                                                                 
  _______      ___      .___  ___.  _______      ______   ____    ____  _______ .______      
 /  _____|    /   \     |   \/   | |   ____|    /  __  \  \   \  /   / |   ____||   _  \     
|  |  __     /  ^  \    |  \  /  | |  |__      |  |  |  |  \   \/   /  |  |__   |  |_)  |    
|  | |_ |   /  /_\  \   |  |\/|  | |   __|     |  |  |  |   \      /   |   __|  |      /     
|  |__| |  /  _____  \  |  |  |  | |  |____    |  `--'  |    \    /    |  |____ |  |\  \----.
 \______| /__/     \__\ |__|  |__| |_______|    \______/      \__/     |_______|| _| `._____|
                                                                                             
");
            Console.Write("Appuyez sur n'importe quelle touche pour quitter");

            switch (Console.ReadKey(true).Key)
            {
                default:
                    break;
            }
            menu.ShowMenu();
        }

        public void Break()
        {
            cursorBreak = 20;

            quitOrStart = false;

            do
            {
                Console.Clear();

                Console.SetCursorPosition(Console.LargestWindowWidth / 2, 0);

                Console.Write(@"
.______   .______       _______     ___       __  ___ 
|   _  \  |   _  \     |   ____|   /   \     |  |/  / 
|  |_)  | |  |_)  |    |  |__     /  ^  \    |  '  /  
|   _  <  |      /     |   __|   /  /_\  \   |    <   
|  |_)  | |  |\  \----.|  |____ /  _____  \  |  .  \  
|______/  | _| `._____||_______/__/     \__\ |__|\__\ 
                                                      
");
                Console.SetCursorPosition(0, 20);

                Console.WriteLine("Reprendre");

                Console.SetCursorPosition(0, 40);

                Console.WriteLine("Quitter");

                Console.SetCursorPosition(20, cursorBreak);

                Console.Write("<--");

                //lis les touches cliquées
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if (cursorBreak == 20)
                        {
                            cursorBreak += 20;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (cursorBreak == 40)
                        {
                            cursorBreak -= 20;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (cursorBreak == 40)
                        {
                            GameOver();
                            quitOrStart = true;
                        }
                        else if (cursorBreak == 20)
                        {
                            Resume();
                            quitOrStart= true;
                        }
                        break;
                    default:
                        break;
                }
            } while (quitOrStart == false);
            
        }

        public void Resume()
        {
            
        }


        public void InitiateAlien()
        {
            if (menu.Difficulty == false)
            {
                numberEnnemiesX = 6;
                numberEnnemiesY = 4;
            }
            else
            {
                numberEnnemiesX = 10;
                numberEnnemiesY = 5;
            }

            for (int X = 0; X < numberEnnemiesX; X++)
            {
                for (int Y = 0; Y < numberEnnemiesY; Y++)
                {

                    Alien alien = new Alien(alienX, alienY);

                    alien.PositionX = X * alien.Width + 5;

                    alien.PositionY = Y * alien.Height + 1;

                    ennemies.Add(alien);
                }
            }
        }

        public void GameMusic()
        {
            WindowsMediaPlayer wMPPlayer = new WindowsMediaPlayer();
            wMPPlayer.URL = AppDomain.CurrentDomain.BaseDirectory + @"/star wars cantina.mp3";
        }

        public void EasterEgg()
        {
            WindowsMediaPlayer wMPPlayer = new WindowsMediaPlayer();
            wMPPlayer.URL = AppDomain.CurrentDomain.BaseDirectory + @"/Better Call Saul Theme by Little Barrie Full Orignal Song.mp3";
        }

        private void KillAlien(Laser laser, Alien alien)
        {
            ennemies.Remove(alien);
            lasersPlayer.Remove(laser);
            laser.Erase();
            alien.Erase();
            player.AddScore();
            Stats(player);
        }

        private void Stats(Player player)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Vie: ");
            for (int nbrLife = 0; nbrLife < player.Life; nbrLife++)
            {
                Console.Write("♥");
            }
            Console.SetCursorPosition(108, 0);
            Console.Write("Spicy Nvaders");
            Console.SetCursorPosition(220, 0);
            Console.Write("Score: " + player.Score);
        }

        public void InitiateWall()
        {
            wall1.Draw();
            wall2.Draw();
            wall3.Draw();
            walls.Add(wall1);
            walls.Add(wall2);
            walls.Add(wall3);
        }
    }
}
