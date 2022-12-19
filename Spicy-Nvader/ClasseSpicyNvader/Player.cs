namespace ClasseSpicyNvader
{
    public class Player : Entity
    {
        //nom du joueur
        private string _name;

        //score du joueur
        private int _score;

        public Player(string name, int positionX, int positionY, byte life)
        {
            //style de l'entitée
            Skin = @"      ▄      ¦     ███     ¦▄███████████▄¦█████████████¦█████████████";

            //nom du joueur
            Name = name;

            //score du joueur
            Score = 0;

            //positionX de l'entitée
            PositionX = positionX;

            //positionY de l'entitée
            PositionY = positionY;

            //nombre de vie du joueur
            Life = life;

            //largeur de l'entitée
            Width = Skin.Split("¦")[0].Length;

            //hauteur de l'entitée
            Height = Skin.Split("¦").Length;
        }

        //propriété du score
        public int Score { get => _score; set => _score = value; }

        //propriété du nom
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// attaque en lançant un laser
        /// </summary>
        /// <returns></returns>
        public Laser Attack()
        {
            Laser laser = new Laser(PositionX + 6, PositionY - 1);
            return laser;
        }

        /// <summary>
        /// permet de se déplacer à droite
        /// </summary>
        public void GoRight()
        {
            if(PositionX != 223)
            {
                Console.MoveBufferArea(PositionX, PositionY, Width, Height, ++PositionX, PositionY);
            }
        }

        /// <summary>
        /// permet de se déplacer à gauche
        /// </summary>
        public void GoLeft()
        {
            if (PositionX != 0)
            {
                Console.MoveBufferArea(PositionX, PositionY, Width, Height, --PositionX, PositionY);
            }
        }

        /// <summary>
        /// perd une vie
        /// </summary>
        public void LoseLife()
        {
            Life--;
        }

        /// <summary>
        /// ajoute des poitns au score
        /// </summary>
        public void AddScore()
        {
            _score += 100;
        }
    }
}