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
        //contient tout les caractères pour dessiner la flèche
        private static string arrow = "   ___              ¦  /  /              ¦ /  / ______ ______ ¦<  < |______|______|¦ \\  \\              ¦  \\__\\             ";

        //contient la flèche découpé en partie
        private string[] subs = arrow.Split("¦");

        //contient la difficulté choisi
        private bool _difficulty = true;

        //contient l'information si on dois jouer la musique
        private bool _playSong = true;

        //permet de savoir si il faut continuer à afficher le menu
        private bool _playing;

        public bool Playing { get => _playing; set => _playing = value; }

        /// <summary>
        /// Affiche le menu de sélection
        /// </summary>
        public void ShowMenu()
        {
            Console.CursorVisible = false;

            //crée une variable pour connaître la position du curseur
            int cursorY = 7;

            _playing = true;

            //dimensionne la console
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            //dimensionne la console
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);


            do
            {
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
                int compteur = 1;

                foreach (string sub in subs)
                {
                    Console.SetCursorPosition(160, cursorY + compteur);
                    Console.Write(sub);
                    compteur++;
                }
                //lis les touches cliquées
                switch (Console.ReadKey(true).Key)
                {
                    //flèche du bas
                    case ConsoleKey.DownArrow:

                        //si le curseur n'est pas sur le dernier choix
                        if (cursorY < 43)
                        {
                            //va sur l'autre choix plus bas
                            Console.MoveBufferArea(160, cursorY, 20, 8, 160, cursorY + 9);

                            cursorY += 9;
                        }
                        else
                        {
                            Console.MoveBufferArea(160, cursorY, 20, 6, 160, cursorY);
                        }

                        //quitte l'action
                        break;

                    //flèche du haut
                    case ConsoleKey.UpArrow:

                        //si le curseur n'est pas sur le premier choix
                        if (cursorY > 7)
                        {
                            //va sur l'autre choix plus bas
                            Console.MoveBufferArea(160, cursorY, 20, 8, 160, cursorY - 9);

                            //va sur le choix au dessus
                            cursorY -= 9;
                        }
                        else
                        {
                            Console.MoveBufferArea(160, cursorY, 20, 6, 160, cursorY);
                        }

                        //quitte l'action
                        break;

                    //si enter
                    case ConsoleKey.Enter:

                        //que le curseur est sur "jouer"
                        if (cursorY == 7)
                        {
                            Game game = new Game();
                            game.StartGame(_playSong, _difficulty);
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

            } while (_playing);
        }

        /// <summary>
        /// montre les informations sur le projet dans une section à part
        /// </summary>
        public void ShowAbout()
        {
            //efface le menu de la console
            Console.Clear();

            //écrit le titre de la page
            Console.WriteLine(@"
     ___         .______   .______        ______   .______     ______        _______.
    /   \        |   _  \  |   _  \      /  __  \  |   _  \   /  __  \      /       |
   /  ^  \       |  |_)  | |  |_)  |    |  |  |  | |  |_)  | |  |  |  |    |   (----`
  /  /_\  \      |   ___/  |      /     |  |  |  | |   ___/  |  |  |  |     \   \    
 /  _____  \     |  |      |  |\  \----.|  `--'  | |  |      |  `--'  | .----)   |   
/__/     \__\    | _|      | _| `._____| \______/  | _|       \______/  |_______/    
                                                                                     
");
            //écrit les informations
            Console.WriteLine("Auteur: Maël Gétain\nSpicy-Nvader est un jeu qui me sers de projet programmation orienté objet. Le projet se déroule d'août à décembre 2022.");
            Console.Write("Appuyez sur n'importe quelle touche pour quitter");

            //permet de quitter la page
            switch (Console.ReadKey().Key)
            {
                default:
                    break;
            }

        }

        /// <summary>
        /// montre les meilleurs résultat fait sur cette ordinateur
        /// </summary>
        public void ShowHighScore()
        {
            //crée un tableau avec tout le contenu du fichier qui contient les résultat
            string[] lines = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "result.txt");

            //efface la console
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

            //écrit tout les éléments du tableau
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            //lis les touches cliquées
            switch (Console.ReadKey(true).Key)
            {
                default:
                    break;
                case ConsoleKey.Escape:
                    break;
            }
        }

        /// <summary>
        /// affiche les options du jeu
        /// </summary>
        public void ShowSettings()
        {
            //crée une variable pour avoir l'information sur sa position
            int cursorY = 7;

            //crée une variable qui nous peremt de rester dans le menu
            bool showSettings = true;

            do
            {
                //efface la console
                Console.Clear();

                //écrit le titre de l'option
                Console.Write(@"
     _______.  ______   .__   __. 
    /       | /  __  \  |  \ |  | 
   |   (----`|  |  |  | |   \|  | 
    \   \    |  |  |  | |  . `  | 
.----)   |   |  `--'  | |  |\   | 
|_______/     \______/  |__| \__| 
                                  
");
                //écrit en vert l'option séléctionnée
                if (_playSong)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(@"
  ______   .__   __. 
 /  __  \  |  \ |  | 
|  |  |  | |   \|  | 
|  |  |  | |  . `  | 
|  `--'  | |  |\   | 
 \______/  |__| \__| 
                     
");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(@"
  ______    _______  _______ 
 /  __  \  |   ____||   ____|
|  |  |  | |  |__   |  |__   
|  |  |  | |   __|  |   __|  
|  `--'  | |  |     |  |     
 \______/  |__|     |__|     
                             
");
                    Console.ResetColor();
                }

                //écrit le titre de l'option
                Console.Write(@"
 _______   __   _______  _______  __    ______  __    __   __      .___________. _______ 
|       \ |  | |   ____||   ____||  |  /      ||  |  |  | |  |     |           ||   ____|
|  .--.  ||  | |  |__   |  |__   |  | |  ,----'|  |  |  | |  |     `---|  |----`|  |__   
|  |  |  ||  | |   __|  |   __|  |  | |  |     |  |  |  | |  |         |  |     |   __|  
|  '--'  ||  | |  |     |  |     |  | |  `----.|  `--'  | |  `----.    |  |     |  |____ 
|_______/ |__| |__|     |__|     |__|  \______| \______/  |_______|    |__|     |_______|
                                                                                         
");
                //écrit en vert l'option séléctionnée
                if (_difficulty)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(@"
 _______    ___       ______  __   __       _______ 
|   ____|  /   \     /      ||  | |  |     |   ____|
|  |__    /  ^  \   |  ,----'|  | |  |     |  |__   
|   __|  /  /_\  \  |  |     |  | |  |     |   __|  
|  |    /  _____  \ |  `----.|  | |  `----.|  |____ 
|__|   /__/     \__\ \______||__| |_______||_______|
                                                    
");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(@"
 _______   __   _______  _______  __    ______  __   __       _______ 
|       \ |  | |   ____||   ____||  |  /      ||  | |  |     |   ____|
|  .--.  ||  | |  |__   |  |__   |  | |  ,----'|  | |  |     |  |__   
|  |  |  ||  | |   __|  |   __|  |  | |  |     |  | |  |     |   __|  
|  '--'  ||  | |  |     |  |     |  | |  `----.|  | |  `----.|  |____ 
|_______/ |__| |__|     |__|     |__|  \______||__| |_______||_______|
                                                                      
");
                    Console.ResetColor();
                }

                //crée un compteur pour compter les cases
                int compteur = 1;

                //écrit la flèche
                foreach (string sub in subs)
                {
                    Console.SetCursorPosition(160, cursorY + compteur);
                    Console.Write(sub);
                    compteur++;
                }

                switch (Console.ReadKey(true).Key)
                {
                    default:
                        break;
                    //flèche du bas
                    case ConsoleKey.DownArrow:

                        //si le curseur n'est pas sur le dernier choix
                        if (cursorY < 25)
                        {
                            Console.MoveBufferArea(160, cursorY, 20, 6, 160, cursorY + 18);

                            //va sur l'autre choix plus bas
                            cursorY += 18;
                        }
                        else
                        {
                            Console.MoveBufferArea(160, cursorY, 20, 6, 160, cursorY);
                        }

                        //quitte l'action
                        break;

                    //flèche du haut
                    case ConsoleKey.UpArrow:

                        //si le curseur n'est pas sur le premier choix
                        if (cursorY > 7)
                        {
                            Console.MoveBufferArea(160, cursorY, 20, 6, 160, cursorY - 18);

                            //va sur le choix au dessus
                            cursorY -= 18;
                        }
                        else
                        {
                            Console.MoveBufferArea(160, cursorY, 20, 6, 160, cursorY);
                        }

                        //quitte l'action
                        break;

                    //sélectionne l'option
                    case ConsoleKey.Enter:
                        if (cursorY == 25 && _difficulty)
                        {
                            _difficulty = false;
                        }
                        else if (cursorY == 25 && !_difficulty)
                        {
                            _difficulty = true;
                        }
                        else if (cursorY == 7 && _playSong)
                        {
                            _playSong = false;
                        }
                        else if (cursorY == 7 && !_playSong)
                        {
                            _playSong = true;
                        }
                        break;
                    case ConsoleKey.Escape:
                        showSettings = false;
                        break;
                }

            } while (showSettings);

        }
    }
}
