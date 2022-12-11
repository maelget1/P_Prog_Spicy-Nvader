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
        int line;
        int numberEnnemiesX;
        int numberEnnemiesY;
        int cursorBreak;
        bool quitOrStart;
        int randomAlien;
        bool playing;
        Player player;
        Random random = new Random();
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
        WindowsMediaPlayer wMPPlayer = new WindowsMediaPlayer();

        /// <summary>
        /// instancie les éléments pour démarrer la partie
        /// </summary>
        /// <param name="sound">son oui ou non</param>
        /// <param name="difficulty">difficulté dur ou facile</param>
        public void InitiateGame(bool sound, bool difficulty)
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
                wMPPlayer.URL = AppDomain.CurrentDomain.BaseDirectory + @"/star wars cantina.mp3";
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
                    randomAlien = random.Next(0, 20);

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
                if(laser.PositionY == 1)
                {
                    lasersPlayer.Remove(laser);
                    laser.Erase();
                }
                else
                {
                    laser.MovePlayer();
                    foreach (Wall wall in walls.ToArray())
                    {
                        if (wall.PositionX <= laser.PositionX && wall.PositionX + wall.Width >= laser.PositionX && wall.PositionY <= laser.PositionY && wall.PositionY + wall.Height >= laser.PositionY)
                        {
                            lasersPlayer.Remove(laser);
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
                    else if (laser.PositionY > 62)
                    {
                        lasersAlien.Remove(laser);
                    }
                }
                if (laser.PositionX >= player.PositionX && laser.PositionX <= player.PositionX + player.Width && laser.PositionY >= player.PositionY && laser.PositionY <= player.PositionY + player.Height)
                {
                    lasersAlien.Remove(laser);
                    laser.Erase();
                    player.LoseLife();
                    Stats(player);
                    player.Draw();
                    if (player.Life == 0)
                    {
                        GameOver();
                    }
                }



            }
        }

        private void GameOver()
        {
            Console.Clear();

            wMPPlayer.close();

            playing = false;

            timerAlien.Dispose();

            timerLaserEnnemies.Dispose();

            timerLaserPlayer.Dispose();

            ennemies.Clear();

            lasersAlien.Clear();

            lasersPlayer.Clear();

            walls.Clear();

            AddBestScore(player.Score, player.Name);

            Console.SetCursorPosition(Console.LargestWindowWidth / 2,0);

            Console.Write(@"                                                                 
  _______      ___      .___  ___.  _______      ______   ____    ____  _______ .______      
 /  _____|    /   \     |   \/   | |   ____|    /  __  \  \   \  /   / |   ____||   _  \     
|  |  __     /  ^  \    |  \  /  | |  |__      |  |  |  |  \   \/   /  |  |__   |  |_)  |    
|  | |_ |   /  /_\  \   |  |\/|  | |   __|     |  |  |  |   \      /   |   __|  |      /     
|  |__| |  /  _____  \  |  |  |  | |  |____    |  `--'  |    \    /    |  |____ |  |\  \----.
 \______| /__/     \__\ |__|  |__| |_______|    \______/      \__/     |_______|| _| `._____|
                                                                                             
");
            Console.WriteLine("Bravo " + player.Name + " vous vous êtes bien battu. Votre score est de " + player.Score + " points");
            Thread.Sleep(1000);
            Console.Write("Appuyez sur n'importe quelle touche pour quitter");

            switch (Console.ReadKey(true).Key)
            {
                default:
                    break;
            }
        }

        private void Break()
        {
            cursorBreak = 20;

            quitOrStart = false;

            do
            {
                timerAlien.Dispose();

                timerLaserEnnemies.Dispose();

                timerLaserPlayer.Dispose();

                walls.Clear();

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
            } while (!quitOrStart);
            
        }

        private void Resume()
        {
            PlayGame();
        }


        private void InitiateAlien(bool difficulty)
        {
            if (difficulty)
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
                    Alien alien = new Alien();

                    alien.PositionX = X * alien.Width + 5;

                    alien.PositionY = Y * alien.Height + 1;

                    ennemies.Add(alien);
                }
            }

            line = ennemies.Max(elements => elements.Height) + ennemies.Max(elements => elements.PositionY);
        }

        private void EasterEgg()
        {
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
            wMPPlayer.URL = AppDomain.CurrentDomain.BaseDirectory + @"/Better Call Saul Theme by Little Barrie Full Orignal Song.mp3";
            Console.ReadKey();
            wMPPlayer.close();
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
            for(int i = 5; i <= 7; i++)
            {
                Console.MoveBufferArea(30, 0, 1, 1, i, 0);
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("Vie: ");
            for (int nbrLife = 0; nbrLife < player.Life; nbrLife++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("♥");
                Console.ResetColor();
            }
            Console.SetCursorPosition(108, 0);
            Console.Write("Spicy Nvaders");
            Console.SetCursorPosition(220, 0);
            Console.Write("Score: " + player.Score);
        }

        private void InitiateWall()
        {
            walls.Add(wall1);
            walls.Add(wall2);
            walls.Add(wall3);
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
