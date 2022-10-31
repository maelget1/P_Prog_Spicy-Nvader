using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace ClasseSpicyNvader
{
    public class Menu
    {
        private List<int> bestScore = new List<int>();

        public List<int> BestScore { get => bestScore; set => bestScore = value; }
        public bool Difficulty { get => difficulty; set => difficulty = value; }

        private bool difficulty;

        public void ShowMenu()
        {
            int cursorY = 7;
            do
            {
                //dimensionne la console
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                //dimensionne la console
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                //efface la console
                Console.Clear();


                //titre
                Console.WriteLine(@"
     _______..______    __    ______ ____    ____      .__   __. ____    ____  ___       _______   _______ .______      
    /       ||   _  \  |  |  /      |\   \  /   /      |  \ |  | \   \  /   / /   \     |       \ |   ____||   _  \     
   |   (----`|  |_)  | |  | |  ,----' \   \/   / ______|   \|  |  \   \/   / /  ^  \    |  .--.  ||  |__   |  |_)  |    
    \   \    |   ___/  |  | |  |       \_    _/ |______|  . `  |   \      / /  /_\  \   |  |  |  ||   __|  |      /     
.----)   |   |  |      |  | |  `----.    |  |          |  |\   |    \    / /  _____  \  |  '--'  ||  |____ |  |\  \----.
|_______/    | _|      |__|  \______|    |__|          |__| \__|     \__/ /__/     \__\ |_______/ |_______|| _| `._____|");

                //écrit l'onglet "jouer"
                Console.WriteLine(@"
       __    ______    __    __   _______ .______      
      |  |  /  __  \  |  |  |  | |   ____||   _  \     
      |  | |  |  |  | |  |  |  | |  |__   |  |_)  |    
.--.  |  | |  |  |  | |  |  |  | |   __|  |      /     
|  `--'  | |  `--'  | |  `--'  | |  |____ |  |\  \----.
 \______/   \______/   \______/  |_______|| _| `._____|
                                                       
");

                //écrit l'onglet "options"
                Console.WriteLine(@"
  ______   .______   .___________. __    ______   .__   __.      _______.
 /  __  \  |   _  \  |           ||  |  /  __  \  |  \ |  |     /       |
|  |  |  | |  |_)  | `---|  |----`|  | |  |  |  | |   \|  |    |   (----`
|  |  |  | |   ___/      |  |     |  | |  |  |  | |  . `  |     \   \    
|  `--'  | |  |          |  |     |  | |  `--'  | |  |\   | .----)   |   
 \______/  | _|          |__|     |__|  \______/  |__| \__| |_______/    
                                                                         
");

                //écrit l'onglet "meilleurs scores"
                Console.WriteLine(@"
.___  ___.  _______  __   __       __       _______  __    __  .______          _______.        _______.  ______   ______   .______       _______     _______.
|   \/   | |   ____||  | |  |     |  |     |   ____||  |  |  | |   _  \        /       |       /       | /      | /  __  \  |   _  \     |   ____|   /       |
|  \  /  | |  |__   |  | |  |     |  |     |  |__   |  |  |  | |  |_)  |      |   (----`      |   (----`|  ,----'|  |  |  | |  |_)  |    |  |__     |   (----`
|  |\/|  | |   __|  |  | |  |     |  |     |   __|  |  |  |  | |      /        \   \           \   \    |  |     |  |  |  | |      /     |   __|     \   \    
|  |  |  | |  |____ |  | |  `----.|  `----.|  |____ |  `--'  | |  |\  \----.----)   |      .----)   |   |  `----.|  `--'  | |  |\  \----.|  |____.----)   |   
|__|  |__| |_______||__| |_______||_______||_______| \______/  | _| `._____|_______/       |_______/     \______| \______/  | _| `._____||_______|_______/    
                                                                                                                                                              
");

                //écrit l'onglet "à propos"
                Console.WriteLine(@"
     ___         .______   .______        ______   .______     ______        _______.
    /   \        |   _  \  |   _  \      /  __  \  |   _  \   /  __  \      /       |
   /  ^  \       |  |_)  | |  |_)  |    |  |  |  | |  |_)  | |  |  |  |    |   (----`
  /  /_\  \      |   ___/  |      /     |  |  |  | |   ___/  |  |  |  |     \   \    
 /  _____  \     |  |      |  |\  \----.|  `--'  | |  |      |  `--'  | .----)   |   
/__/     \__\    | _|      | _| `._____| \______/  | _|       \______/  |_______/    
                                                                                     
");

                //écrit l'onglet "switch"
                Console.WriteLine(@"
  ______      __    __   __  .___________.___________. _______ .______      
 /  __  \    |  |  |  | |  | |           |           ||   ____||   _  \     
|  |  |  |   |  |  |  | |  | `---|  |----`---|  |----`|  |__   |  |_)  |    
|  |  |  |   |  |  |  | |  |     |  |        |  |     |   __|  |      /     
|  `--'  '--.|  `--'  | |  |     |  |        |  |     |  |____ |  |\  \----.
 \_____\_____\\______/  |__|     |__|        |__|     |_______|| _| `._____|
                                                                            
");

                //place le curseur à côté du 1 er onglet
                Console.SetCursorPosition(160, cursorY + 1);

                //écrit le curseur
                Console.WriteLine("   ___              ");

                Console.SetCursorPosition(160, cursorY + 2);

                Console.WriteLine("  /  /              ");

                Console.SetCursorPosition(160, cursorY + 3);

                Console.WriteLine(" /  / ______ ______ ");

                Console.SetCursorPosition(160, cursorY + 4);

                Console.WriteLine("<  < |______|______|");

                Console.SetCursorPosition(160, cursorY + 5);

                Console.WriteLine(" \\  \\              ");

                Console.SetCursorPosition(160, cursorY + 6);

                Console.WriteLine("  \\__\\             ");


                //lis les touches cliquées
                switch (Console.ReadKey().Key)
                {
                    //flèche du bas
                    case ConsoleKey.DownArrow:

                        //si le curseur n'est pas sur le dernier choix
                        if (cursorY < 43)
                        {
                            //va sur l'autre choix plus bas
                            cursorY += 9;
                        }

                        //quitte l'action
                        break;

                    //flèche du haut
                    case ConsoleKey.UpArrow:

                        //si le curseur n'est pas sur le premier choix
                        if (cursorY > 7)
                        {
                            //va sur le choix au dessus
                            cursorY -= 9;
                        }

                        //quitte l'action
                        break;

                    //si enter
                    case ConsoleKey.Enter:

                        //que le curseur est sur "jouer"
                        if (cursorY == 7)
                        {
                            Game game = new Game();
                            game.PlayGame();
                        }

                        //si sur l'option "options"
                        else if (cursorY == 16)
                        {
                            ShowSettings();
                        }

                        //si sur l'option "meilleurs scores"
                        else if (cursorY == 25)
                        {
                            ShowHighScore();
                        }

                        //si sur l'option "a propos"
                        else if (cursorY == 34)
                        {
                            ShowAbout();
                        }

                        //si sur l'option "quitter"
                        else if (cursorY == 43)
                        {
                            Environment.Exit(0);
                        }

                        //quitte l'action
                        break;

                    //par défault donc si aucun cas n'est valide
                    default:

                        //quitte l'action
                        break;
                }

            } while (true);
        }

        public void ShowAbout()
        {

        }

        public void ShowHighScore()
        {
            int cursorY = 10;
            int nbrElements = 10;
            Console.Clear();
            //écrit l'onglet "meilleurs scores"
            Console.WriteLine(@"
.___  ___.  _______  __   __       __       _______  __    __  .______          _______.        _______.  ______   ______   .______       _______     _______.
|   \/   | |   ____||  | |  |     |  |     |   ____||  |  |  | |   _  \        /       |       /       | /      | /  __  \  |   _  \     |   ____|   /       |
|  \  /  | |  |__   |  | |  |     |  |     |  |__   |  |  |  | |  |_)  |      |   (----`      |   (----`|  ,----'|  |  |  | |  |_)  |    |  |__     |   (----`
|  |\/|  | |   __|  |  | |  |     |  |     |   __|  |  |  |  | |      /        \   \           \   \    |  |     |  |  |  | |      /     |   __|     \   \    
|  |  |  | |  |____ |  | |  `----.|  `----.|  |____ |  `--'  | |  |\  \----.----)   |      .----)   |   |  `----.|  `--'  | |  |\  \----.|  |____.----)   |   
|__|  |__| |_______||__| |_______||_______||_______| \______/  | _| `._____|_______/       |_______/     \______| \______/  | _| `._____||_______|_______/    
                                                                                                                                                              
");
            foreach(int elements in BestScore)
            {
                Console.SetCursorPosition(10, nbrElements);
                Console.WriteLine(elements);
                nbrElements += 10;
            }
            //lis les touches cliquées
            switch (Console.ReadKey().Key)
            {
                default:
                    break;
                case ConsoleKey.DownArrow:
                    cursorY += 10;
                    break;
            }
        }

        public void AddBestScore(int score)
        {
            BestScore.Add(score);
        }

        public void ShowSettings()
        {
            Difficulty = false;
        }
    }
}
