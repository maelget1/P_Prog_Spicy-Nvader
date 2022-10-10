using ClasseSpicyNvader;

///////////////////////////////////////
// Auteur: Maël Gétain               //
// Date: 03.10.2022                  //
// Description: Partie principale du //
// projet Spicy-Nvader.              //
///////////////////////////////////////

/////////////////////////////////////////////////////variables et constantes/////////////////////////////////////////////////////

int cursorY = 7;
string name;
Player player;

/////////////////////////////////////////////////////Programme principal/////////////////////////////////////////////////////

do
{
    //dimensionne la console
    Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

    //dimensionne la console
    Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

    //efface la console
    Console.Clear();

    //écrit le titre
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
    Console.SetCursorPosition(160, cursorY+1);

    //écrit le curseur
    Console.WriteLine("   ___              ");

    Console.SetCursorPosition(160, cursorY+2);

    Console.WriteLine("  /  /              ");

    Console.SetCursorPosition(160, cursorY+3);

    Console.WriteLine(" /  / ______ ______ ");

    Console.SetCursorPosition(160, cursorY+4);

    Console.WriteLine("<  < |______|______|");

    Console.SetCursorPosition(160, cursorY+5);

    Console.WriteLine(" \\  \\              ");

    Console.SetCursorPosition(160, cursorY+6);

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
            if(cursorY > 7)
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
                //efface la console
                Console.Clear();

                //place le curseur au milieu de l'écran
                Console.SetCursorPosition(Console.LargestWindowWidth/2, Console.LargestWindowHeight/2);

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
                     player = new Player(name, 0, 50, 54, 5);
                }

                //si le pseudo est autres
                else
                {
                    //instancie un nouveau joueur avec le nom qu'il a entré
                    player = new Player(name, 0, 50, 54, 3);
                }

                //le fait tant que c'est pas fini
                do
                {

                    player.draw();

                    //lis les touches cliquées
                    switch (Console.ReadKey().Key)
                    {
                        //si flèche de droite
                        case ConsoleKey.RightArrow:

                            //appele la fonction qui permet d'aller à droite
                            player.goRight();

                            //quitte l'action
                            break;

                        //si flèche de gauche
                        case ConsoleKey.LeftArrow:

                            //appelle la fonction pour aller à gauche
                            player.goLeft();

                            //quitte l'action
                            break;

                        //si espace
                        case ConsoleKey.Spacebar:

                            //appelle la fonction pour tirer
                            player.shot();

                            //quitte l'action
                            break;

                        //si escape
                        case ConsoleKey.Escape:

                            //va faire toute la scène de pause et sauvegarder les variables ( positions ennemis position missiles pv etc...)

                            //quitte l'action
                            break;
                    }
                } while (true);
            }

            //si sur l'option "options"
            else if(cursorY == 16)
            {
                Console.Clear();
            }

            //si sur l'option "meilleurs scores"
            else if (cursorY == 25){
                Console.Clear();
            }

            //si sur l'option "a propos"
            else if (cursorY == 34)
            {
                Console.Clear();
            }

            //si sur l'option "quitter"
            else if(cursorY == 43)
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

