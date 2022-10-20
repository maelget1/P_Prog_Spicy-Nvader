using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClasseSpicyNvader
{
    public class Game
    {
        //////////////////////////////////////////////////variables//////////////////////////////////////////////////
        string name;
        Player player;

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
            else
            {
                //instancie un nouveau joueur avec le nom qu'il a entré
                player = new Player(name, 0, 107, 56, 3);
            }

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
                        player.Attack();

                        //quitte l'action
                        break;

                    //si escape
                    case ConsoleKey.Escape:

                        GameOver();

                        //quitte l'action
                        break;

                    default:
                        break;
                }
            } while (true);
        }

        public void GameOver()
        {
            Console.Clear();

            player.AddBestScore();

            Console.SetCursorPosition(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2);

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
                    Menu menu = new Menu();
                    menu.ShowMenu();
                    break;
            }
        }

        
    }
}
