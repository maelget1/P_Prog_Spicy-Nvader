using System;
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
        Player player;
        Menu menu = new Menu();
        Wall wall1 = new Wall();
        Wall wall2 = new Wall();
        Wall wall3 = new Wall();
        List<Alien> ennemies = new List<Alien>();
        List<Laser> lasers = new List<Laser>();
        Timer timer;       

        public void PlayGame()
        {
            

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
                player = new Player(name, 0, 107, 56, 5);
            }

            //si le pseudo est autres
            else if(name == "Saul")
            {
                EasterEgg();

                //instancie un nouveau joueur avec le nom qu'il a entré
                player = new Player(name, 0, 107, 56, 3);
            }

            else
            {
                //instancie un nouveau joueur avec le nom qu'il a entré
                player = new Player(name, 0, 107, 56, 3);
            }
            
            InitiateAbout(player);

            InitiateAlien();

            timer = new Timer(new TimerCallback(ActionEnemies));
            timer.Change(0, 200);

            GameMusic();

            wall1.DrawOnce(30, 50);
            wall2.DrawOnce(105, 50);
            wall3.DrawOnce(180, 50);

            //le fait tant que c'est pas fini
            do
            {
                player.Draw();

                //lis les touches cliquées
                switch (Console.ReadKey().Key)
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
                        lasers.Add(player.Attack());

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
            } while (true);
        }

        private void ActionEnemies(object state)
        {
            int minX = ennemies.Min(e => e.PositionX);
            int bigX = ennemies.Max(elements => elements.PositionX);
            bigX = ennemies.Max(elements => elements.Width) + bigX;
            int minY = ennemies.Max(elements => elements.PositionY);
            int bigY = ennemies.Max(elements => elements.PositionY);
            bigY = ennemies.Max(elements => elements.Height) + bigY;
            foreach (Alien elements in ennemies.ToArray())
            {
                
                elements.Draw();
                if (minX <= 240 - (elements.Width * 6) && bigY % 2 == 1)
                {
                    if (minX == 240 - (elements.Width * 6))
                    {
                        elements.MoveDown();
                    }
                    else
                    {
                        elements.MoveRight();
                    }
                }
                else if (bigX >=elements.Width * 6 && bigY % 2 == 0)
                {
                    if (minX == 0)
                    {
                        elements.MoveDown();
                    }
                    else
                    {
                        elements.MoveLeft();
                    }
                }
                foreach (Laser laser in lasers.ToArray())
                {
                    if (elements.PositionX <= laser.PositionX && elements.PositionX + elements.Width >= laser.PositionX && elements.PositionY + elements.Height == laser.PositionY)
                    {
                        ennemies.Remove(elements);
                        lasers.Remove(laser);
                    }
                }
            }
        }

        public void GameOver()
        {
            Console.Clear();

            menu.AddBestScore(player.Score);

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

            switch (Console.ReadKey().Key)
            {
                default:
                    menu.ShowMenu();
                    break;
            }
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
                switch (Console.ReadKey().Key)
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

        public void InitiateAbout(Player player)
        {
            Console.Write("Vie: ");
            for(int nbrLife = 0; nbrLife < player.Life; nbrLife++)
            {
                Console.Write("♥");
            }
            Console.Write("\t\t\t\t\t\t\t\t\t\t\t\t\t Spicy Nvaders \t\t\t\t\t\t\t\t\t\t\t\t\t Score: " + player.Score + "\n");
        }

        public void AlienDie(Alien alien)
        {
            ennemies.Remove(alien);
        }
    }
}
