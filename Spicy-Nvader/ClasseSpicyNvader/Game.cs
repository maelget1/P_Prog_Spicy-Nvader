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
        string name;                                                        //nom du joueur
        int line;                                                           //coordonée y du dernier alien                                              
        bool playing;                                                       //permet de savoir si on continue de jouer
        Player player;                                                      //joueur de la partie
        Random random = new Random();                                       //instancie un random
        Wall wall1 = new Wall(30, 50);                                      //instancie le mur 1
        Wall wall2 = new Wall(100, 50);                                     //instancie le mur 2
        Wall wall3 = new Wall(150, 50);                                     //instancie le mur 3
        List<Alien> ennemies = new List<Alien>();                           //créer une liste d'ennemis
        List<Laser> lasersPlayer = new List<Laser>();                       //créer une liste de laser ennemis
        List<Wall> walls = new List<Wall>();                                //créer une liste de mur
        List<Laser> lasersAlien = new List<Laser>();                        //créer une liste de laser pour le joueur
        Timer timerAlien;                                                   //créer un timer pour les mouvements des aliens
        Timer timerLaserPlayer;                                             //créer un timer pour les mouvements des lasers du joueur
        Timer timerLaserEnnemies;                                           //créer un timer pour les mouvements des lasers ennemis
        WindowsMediaPlayer wMPPlayer = new WindowsMediaPlayer();            //instancie un joueur de musique
        

        /// <summary>
        /// instancie les éléments pour démarrer la partie
        /// </summary>
        /// <param name="sound">son oui ou non</param>
        /// <param name="difficulty">difficulté dur ou facile</param>
        public void InitiateGame(bool sound, bool difficulty)
        {
            //efface la console
            Console.Clear();

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            if (sound)
            {
                //set le fichier dans lequel se trouve les musiques
                Directory.SetCurrentDirectory(@"..\..\..\ressources\");
            }    

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
            else if (name == "Saul")
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

            //dit que le jeu est en marche
            playing = true;

            //initialise les aliens en fonction de la difficulté
            InitiateAlien(difficulty);

            //si le son est sur "on"
            if (sound)
            {
                //lance la musique de jeu
                wMPPlayer.URL = Directory.GetCurrentDirectory() + @"\cantina.wav";
            }
        }

        /// <summary>
        /// partie qui va intéragir avec l'utilisateur et changer en cours de partie 
        /// </summary>
        public void PlayGame()
        {
            //efface la console
            Console.Clear();

            //écrit les murs
            InitiateWall();

            //affiche les stats du joueur (vie et points)
            Stats(player);

            //dessine tout les laser tiré par les ennemies
            foreach (Laser laser in lasersAlien)
            {
                laser.Draw();
            }

            //dessine tout les lasers de l'utilisateur
            foreach(Laser laser in lasersPlayer)
            {
                laser.Draw();
            }

            //dessine le vaisseau
            player.Draw();

            //instancie le timer avec comme action le mouvement des aliens
            timerAlien = new Timer(new TimerCallback(ActionEnemies));

            //change les batements du timer
            timerAlien.Change(0, 200);

            //instancie le timer avec comme action le mouvement des lasers du joueur
            timerLaserPlayer = new Timer(new TimerCallback(LaserMovementPlayer));

            //change les batements du timer
            timerLaserPlayer.Change(0, 50);

            //instancie le timer avec comme action le mouvement des lasers des aliens
            timerLaserEnnemies = new Timer(new TimerCallback(LaserMovementEnnemies));

            //change les batements du timer
            timerLaserEnnemies.Change(0, 100);

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

                        if(lasersPlayer.Count != 1)
                        {
                            //appelle la fonction pour tirer
                            lasersPlayer.Add(player.Attack());
                        }

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

        /// <summary>
        /// appelle l'instanciation et l'action de jeu
        /// </summary>
        /// <param name="sound">son oui ou non</param>
        /// <param name="difficulty">difficulté dur ou facile</param>
        public void StartGame(bool sound, bool difficulty)
        {
            InitiateGame(sound, difficulty);
            PlayGame();
        }

        /// <summary>
        /// déplacement des ennemies
        /// </summary>
        /// <param name="state"></param>
        private void ActionEnemies(object state)
        {
            //si il y a des ennemies
            if(ennemies.Count != 0)
            {
                //pour savoir si les aliens doivent descendre
                bool down = false;

                //calcule la coordonnée x du 1 er alien
                int minX = ennemies.Min(elements => elements.PositionX);

                //calcule la coordonnée x du dernier alien de la liste
                int bigX = ennemies.Max(elements => elements.PositionX);

                //calcule la coordonnée x de fin de la liste d'alien
                bigX = bigX + ennemies.Max(elements => elements.Width);

                //calcule la coordonnée y du dernier alien de la liste
                int bigY = ennemies.Max(elements => elements.Height);

                //calcule la coordonnée y de fin de la liste d'alien
                bigY = bigY + ennemies.Max(elements => elements.PositionY);

                //fait l'action pour tous les ennemies
                foreach (Alien element in ennemies.ToArray())
                {
                    //dessine l'ennemi
                    element.Draw();

                    //prend un chiffre entre 0 et 19
                    int randomAlien = random.Next(0, 20);

                    //si il est toujours dans la fenêtre par rapport à la droite
                    if (minX <= 240 - (element.Width * ((bigX-minX) / element.Width)) && line % 2 == 1)
                    {
                        //si il touche le bord il descend
                        if (minX == 240 - (element.Width * ((bigX - minX) / element.Width)))
                        {
                            element.MoveDown();
                            down = true;
                        }
                        //sinon ils continuent d'aller à droite
                        else
                        {
                            element.MoveRight();

                            //si il est sur la dernière liste et que le random l'a choisi
                            if (element.PositionY + element.Height == bigY && randomAlien == 3)
                            {
                                //il tire
                                lasersAlien.Add(element.Attack());
                            }
                        }
                    }
                    //si il est toujours dans les limites par rapport à la gauche
                    else if (bigX >= element.Width * ((bigX - minX) / element.Width) && line % 2 == 0)
                    {
                        //si il touche le bord
                        if (minX == 0)
                        {
                            //descend les aliens
                            element.MoveDown();
                            down = true;
                        }
                        //sinon
                        else
                        {
                            //va à gauche
                            element.MoveLeft();

                            //si il est sur la dernière liste et que le random l'a choisi
                            if (element.PositionY + element.Height == bigY && randomAlien == 3)
                            {
                                //il tire
                                lasersAlien.Add(element.Attack());
                            }
                        }
                    }

                    //le fait pour tout les aliens du joueur
                    foreach (Laser laser in lasersPlayer.ToArray())
                    {
                        //si le laser est à la même place qu'un alien alors il meurt
                        if (element.PositionX <= laser.PositionX && element.PositionX + element.Width >= laser.PositionX && element.PositionY <= laser.PositionY && element.PositionY + element.Height >= laser.PositionY)
                        {
                            KillAlien(laser, element);
                        }
                    }
                    //si les aliens touchent le bas alors on perd
                    if (line == 50)
                    {
                        GameOver();
                    }
                }
                if (down)
                {
                    line++;
                }

            }
            //si il n'y a plus d'alien fin de partie
            else
            {
                GameOver();
            }
        }

        /// <summary>
        /// mouvement des lasers du joueur
        /// </summary>
        /// <param name="state"></param>
        private void LaserMovementPlayer(object state)
        {
            foreach(Laser laser in lasersPlayer.ToArray())
            {
                //efface le laser si il touche le ciel
                if(laser.PositionY == 1)
                {
                    lasersPlayer.Remove(laser);
                    laser.Erase();
                }
                else
                {
                    //bouge le laser
                    laser.MovePlayer();
                    foreach (Wall wall in walls.ToArray())
                    {
                        //si il tir sur un mur
                        if (wall.PositionX <= laser.PositionX && wall.PositionX + wall.Width >= laser.PositionX && wall.PositionY <= laser.PositionY && wall.PositionY + wall.Height >= laser.PositionY && wall.Life != 0)
                        {
                            //efface le laser
                            lasersPlayer.Remove(laser);
                            laser.Erase();

                            //mets des degats
                            wall.TakeDamage(walls);

                            //redessine le mur avec la bonne couleur
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
                }
            }
        }

        /// <summary>
        /// mouvement des laser des aliens
        /// </summary>
        /// <param name="state"></param>
        private void LaserMovementEnnemies(object state)
        {
            foreach (Laser laser in lasersAlien.ToArray())
            {
                //bouge le laser
                laser.MoveAlien();
                foreach (Wall wall in walls.ToArray())
                {
                    //si il touche le mur
                    if (wall.PositionX <= laser.PositionX && wall.PositionX + wall.Width >= laser.PositionX && wall.PositionY <= laser.PositionY && wall.PositionY + wall.Height >= laser.PositionY)
                    {
                        //efface le laser
                        lasersAlien.Remove(laser);
                        laser.Erase();

                        //mets des dégats
                        wall.TakeDamage(walls);

                        //redessine le mur avec la bonne couleur
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
                    // si il touche le sol il supprime le laser
                    else if (laser.PositionY > 62)
                    {
                        lasersAlien.Remove(laser);
                    }
                }
                //si c'est la même position que le joueur
                if (laser.PositionX >= player.PositionX && laser.PositionX <= player.PositionX + player.Width && laser.PositionY >= player.PositionY && laser.PositionY <= player.PositionY + player.Height)
                {
                    //enlève le laser
                    lasersAlien.Remove(laser);
                    laser.Erase();

                    //enlève un point de vie
                    player.LoseLife();

                    //affiche les stats et redessine le joueur
                    Stats(player);
                    player.Draw();

                    //si plus de vie alors fin de partie
                    if (player.Life == 0)
                    {
                        GameOver();
                    }
                }



            }
        }

        /// <summary>
        /// fin du jeu
        /// </summary>
        private void GameOver()
        {
            //efface l'écran
            Console.Clear();

            //arrête la musique
            wMPPlayer.close();

            //arrête le jeu
            playing = false;

            //arrête les mouvements des aliens
            timerAlien.Dispose();

            //arrête les mouvements des laser aliens
            timerLaserEnnemies.Dispose();

            //arrête les mouvements de nos lasers
            timerLaserPlayer.Dispose();

            //vide la liste d'ennemis
            ennemies.Clear();

            //vide la liste de laser
            lasersAlien.Clear();

            //vide la liste de laser du joueur
            lasersPlayer.Clear();

            //vide la liste de mur
            walls.Clear();

            //ajoute le meilleur score
            AddBestScore(player.Score, player.Name);

            //place le curseur
            Console.SetCursorPosition(Console.LargestWindowWidth / 2,0);

            //titre de la fenêtre
            Console.Write(@"                                                                 
  _______      ___      .___  ___.  _______      ______   ____    ____  _______ .______      
 /  _____|    /   \     |   \/   | |   ____|    /  __  \  \   \  /   / |   ____||   _  \     
|  |  __     /  ^  \    |  \  /  | |  |__      |  |  |  |  \   \/   /  |  |__   |  |_)  |    
|  | |_ |   /  /_\  \   |  |\/|  | |   __|     |  |  |  |   \      /   |   __|  |      /     
|  |__| |  /  _____  \  |  |  |  | |  |____    |  `--'  |    \    /    |  |____ |  |\  \----.
 \______| /__/     \__\ |__|  |__| |_______|    \______/      \__/     |_______|| _| `._____|
                                                                                             
");
            //message de victoire
            Console.WriteLine("Bravo " + player.Name + " vous vous êtes bien battu. Votre score est de " + player.Score + " points");

            //petite pause
            Thread.Sleep(1000);

            //message de sortie
            Console.Write("Appuyez sur n'importe quelle touche pour quitter");

            //attends un clique pour quitter
            Console.ReadKey(true);
        }

        /// <summary>
        /// mets le jeu en pause
        /// </summary>
        private void Break()
        {
            //place le curseur
            int cursorBreak = 20;

            //mets que l'utilisateur reste dans la fenêtre de pause
            bool quitOrStart = false;

            do
            {
                //stop le mouvement des aliens
                timerAlien.Dispose();

                //stop le mouvement des lasers des aliens
                timerLaserEnnemies.Dispose();

                ////stop le mouvement de nos lasers
                timerLaserPlayer.Dispose();

                //vide la liste de mur
                walls.Clear();

                //efface l'écran
                Console.Clear();

                //place le curseur
                Console.SetCursorPosition(Console.LargestWindowWidth / 2, 0);

                //titre fenêtre
                Console.Write(@"
.______   .______       _______     ___       __  ___ 
|   _  \  |   _  \     |   ____|   /   \     |  |/  / 
|  |_)  | |  |_)  |    |  |__     /  ^  \    |  '  /  
|   _  <  |      /     |   __|   /  /_\  \   |    <   
|  |_)  | |  |\  \----.|  |____ /  _____  \  |  .  \  
|______/  | _| `._____||_______/__/     \__\ |__|\__\ 
                                                      
");
                //place sous-titre
                Console.SetCursorPosition(0, 20);

                //écrit sous-titre
                Console.WriteLine("Reprendre");

                //place sous-titre
                Console.SetCursorPosition(0, 40);

                //écrit sous-titre
                Console.WriteLine("Quitter");

                //place la flèche
                Console.SetCursorPosition(20, cursorBreak);

                //écrit la flèche
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
            } while (!quitOrStart);
            
        }

        /// <summary>
        /// reprend la partie
        /// </summary>
        private void Resume()
        {
            //continue le jeu
            PlayGame();
        }

        /// <summary>
        /// initialise les aliens
        /// </summary>
        /// <param name="difficulty"></param>
        private void InitiateAlien(bool difficulty)
        {
            int numberEnnemiesX;
            int numberEnnemiesY;

            //si la difficulté est padawan
            if (difficulty)
            {
                numberEnnemiesX = 6;
                numberEnnemiesY = 4;
            }
            //si elle est jedi. plus d'ennemis
            else
            {
                numberEnnemiesX = 10;
                numberEnnemiesY = 5;
            }

            //créer les aliens les mets dans la liste et les places
            for (int X = 0; X < numberEnnemiesX; X++)
            {
                for (int Y = 0; Y < numberEnnemiesY; Y++)
                {
                    Alien alien = new Alien(X, Y);

                    ennemies.Add(alien);
                }
            }

            //pour connaître la position y de la dernière ligne d'aliens
            line = ennemies.Max(elements => elements.Height) + ennemies.Max(elements => elements.PositionY);
        }

        /// <summary>
        /// permet de jouer l'easterEgg de Saul
        /// </summary>
        private void EasterEgg()
        {
            //dessine Saul
            Console.Write(@"                                     ......................................,,,,,,,,,,,,,********///(
                                ....              .........,.............,,,,,,,,,,,,,********/////(
                             ....... ...             ........,,*,.....,,,,,,,,,,,,,*********/////(((
                             ........   ....          ........,,/*/(.,.,,,,,,,,,,,*******//////(((((
                           .      ..............................,***(##(.,,,,,,,*******//////(((((((
                         .      .,*//****,,,,,,,,,,,,,,........,,,,*/##@&(,,,,********/////((((((###
                       . .    ,*/////////////****,,,,,,,,.....,,,,*/#(#&&%**,******//////(((((((####
                     ....    *////////////////*****,,,,,,,,.....,**(%@&##%/,******/////((((((((#####
                    ... .  .,///////////////////*******,,,,,,....,//#@@%(%&&*****//////(((((((#####%
                 ......   .,*////////////////////********,,,,,,..,,*#&@%(*#&@(**//////(((((((#####%%
             ..  . ....  ..*////(((((/(////////////********,,,,,.,,,(@@&(/*(@@%///////((((((######%%
        . ..... ......  .,,///((((((((((///////////********,,,,,,,,,*%@&#/*(%@@&/////((((((######%%%
         ................,,*(((((((((((/////////////*******,,,,,,,,,,(@@%((##%@@(////(((((((####%%%%
........      ........ ,,,**/((((((((((//////////////*****,,,,,,,,,,,,%@@%#(%&@@&////((((((#####%%%%
   .......  . .........*****//(((((((((((/////////////*****,,,,,,,,,,*&@@&#%%&@@@////(((((######%%%%
            . ........,**////(((((((((((((/////////////****,,,,,,,,,,*%@@&(&&@@@&///((((((######%%%%
               .... ..,*/////(((((((((((((((/////((/////****,,,,,,,,*#@@@&(%&@@@%///((((((######%%%%
               .    ..***//////*/((((((((((((/((*(((////***,,,,,,,,**%@@@@#%@@@&((/(((((((######%%%%
        .....  .     .******,,,,,,...,,*//////(/**((//**,,,,..,,,,,,,/%&@@#&@&@%///((((((((#####%%%%
      ........       ,*****//((((((((//*,,***(((*,,,,,...,**///***,,,*(%@@%%&#@(////(((((((#####%%%%
        .......      ,*****(((/,... ...,,*/(((((/,........,,/,,,,,,,,*,*&@&(###(////((((((((####%%%%
       ........     .,**/////*******,***//(/((((/,,.....,,*****,,,,,,,,,%@@(%&(#/////(((((((#####%%%
     .........      .,**////(((((((/////*/(((((/**,.....,,,****,,,,,,,,,(&@#%&%(//////(((((((#####%%
      .......       .,**//(((((####((///(((((((//*,......,,,,****,,,,,,*#@@((#/%///////(((((((#####%
      .......      . .,*//((((((((((((##((((((((/*........,,,,,*****,,,,#@%/#/*#/////////((((((#####
  ............      ..,*//(((((##((((((#((((((((/*.........,,,,,,,,,,,,,#@%(&*(#**/////////(((((####
..............      ..,,*///((((((((((##((((((((/*,.........,,,,,,,,,,,,(@@@%**%****////////((((((##
  ............   ....,,,*////(((#(((((((((((((#(//*.........,,,,,,,,,,,,#@@@#,(********///////((((((
  ...................,,,*////(((((((((((((/**((///*,.......,,,,,,,,.**,*#&@@(*************//////((((
   ...................,,**////((((((((((((((////**,.........,,,,,,.,*,,/#%&@,*,,*************//////(
  ....................,,,*////(((((((((((//(((/*,(/,..........,,,..,,**/((%%,,,,,,,,***********/////
  .....................,,**////(((/////////////,*//**,,,,.......,..,,,*//*#,,,,,,,,,,,,,*********///
   .....................,**////((//////((((((((///**,,,,.........,.,*,**,*,,,,,,,,,,,,,,,,**********
  .......................,**///((//////((/////*,,,,,,,,........,,,.,,,,**,,,,,,,,,,,,,,,,,,,,*******
      ....................,***///////////////******,,,,........,,..,,,**,,,,,,,,,,,,,,,,,,,,,,,,,,**
     .....................,,****/////////////*******,,,............,,,,(,,,,,,,,,,,,,,,,,,,,,,,,,,,,
        ..................,,,***////////////(//////****,,,,.........,,,//*......,,,..,,,,,,,,,,,,,,,
             ...........   ,,,,****/////(////(/////***,,,,.........,.../& ,..............,,,,,,,,,,,
            ...   .        ,,,,,******/////////******,,,..........,..../%% ,#,................,,,,,,
                           ,,,,,*,*****////*****,,,,,,,,,............../#( .,(/,,/*,................
                           ,,,**,*********,,,,,,,,,....................*(,  .///,,.,,..,*(*.........
                           ,,,****,*********,,,,,,,,....................,.  ..*/*,,.........,*(#*...
                           ,,*****///*////********,,,,,,...... ..........    ..,/,,..............,/(
                            ,*****//////**/******//**,,,................    .....,*,.......,........
                            .******////(((//********,,..,/*,,.........       .......................
                             .*****/////((((((/**,,,,///*,,,,.....         .      ..................
                              .*****////((((((# . ./((**,,,,.....      .. .,      ..................
                               .****////((((( *(#/(/*,,.*,......    ..... .,          ..............
                               .,***/////((.,..***.*,...  *.....  ...... .,,          . .*      . ..
                               ..****///(,  .,.,,, ... .    *.......... .,**.         .  . .        
                                .,***//, ...*/./%#*        ..,*.......  .***.                       
                                .,**/,  ..,,/(,(&,.       ..,,***....  .,***.                       
                                 .*, .,,,,*,*/,#%/.      ...,****,,.  ,,****.                       
                                  .,,,,,**,.*((#/*       ...,***,,...,,,****.                       
");

            //lance la musique de la série
            wMPPlayer.URL = Directory.GetCurrentDirectory() + @"\easterEgg.wav";

            //attends un clique
            Console.ReadKey();

            //arrête la musique
            wMPPlayer.close();
        }

        /// <summary>
        /// tue un alien précis et efface le laser qui a tué l'alien
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="alien"></param>
        private void KillAlien(Laser laser, Alien alien)
        {
            //tue l'alien
            ennemies.Remove(alien);

            //supprime le laser
            lasersPlayer.Remove(laser);

            //efface le laser
            laser.Erase();

            //efface l'alien
            alien.Erase();

            //ajoute des points
            player.AddScore();

            //affiche les nouvelles stats
            Stats(player);
        }

        /// <summary>
        /// affiche les statistiques du joueurs (donc la première ligne de la console)
        /// </summary>
        /// <param name="player"></param>
        private void Stats(Player player)
        {
            //efface les coeurs si ils existent
            for(int i = 5; i <= 7; i++)
            {
                Console.MoveBufferArea(30, 0, 1, 1, i, 0);
            }
            
            //place le curseur
            Console.SetCursorPosition(0, 0);

            //écrit le points de vie
            Console.Write("Vie: ");
            for (int nbrLife = 0; nbrLife < player.Life; nbrLife++)
            {
                //couleur rouge
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("♥");
                Console.ResetColor();
            }

            //place le curseur
            Console.SetCursorPosition(108, 0);

            //titre
            Console.Write("Spicy Nvaders");

            //place le curseur
            Console.SetCursorPosition(220, 0);

            //score du joueur
            Console.Write("Score: " + player.Score);
        }

        /// <summary>
        /// va initialiser tout les murs avec les bon points de vie
        /// </summary>
        private void InitiateWall()
        {
            //ajoute les murs à la liste
            walls.Add(wall1);
            walls.Add(wall2);
            walls.Add(wall3);

            //dessine tout les murs avec la bonne couleur
            foreach(Wall wall in walls)
            {
                if(wall.Life == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    wall.Draw();
                    Console.ResetColor();
                }
                else if(wall.Life == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    wall.Draw();
                    Console.ResetColor();
                }
                else if(wall.Life == 3)
                {
                    wall.Draw();
                }
            }
        }

        /// <summary>
        /// ajoute le score au fichier texte
        /// </summary>
        /// <param name="score">score du joueur</param>
        /// <param name="name">nom du joueur</param>
        private async void AddBestScore(int score, string name)
        {
            if(player.Score != 0)
            {
                //si le fichier texte existe
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "result.txt"))
                {
                    //créer le nouveau score
                    string text = name + " :" + score + "\n";

                    //écrit le score dans le fichier texte
                    await File.WriteAllTextAsync("result.txt", text);
                }
                // si il existe pas
                else
                {
                    //créer le nouveau score
                    string text = name + " :" + score + "\n";

                    //créer le fichier texte
                    using StreamWriter file = new("result.txt", append: true);

                    //écrit le score dans le fichier texte
                    await file.WriteLineAsync(text);
                }
            }

        }
    }
}
