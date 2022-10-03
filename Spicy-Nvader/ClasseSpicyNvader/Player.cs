namespace ClasseSpicyNvader
{
    public class Player : Entity
    {
        public string name;

        private int score;

        public Player(string name, int score, int positionX, int positionY, byte life)
        {
            this.name = name;

            Score = score;

            PositionX = positionX;

            PositionY = positionY;

            Life = life;
            
        }

        public int Score { get => score; set => score = value; }

        public void attack()
        {
            Laser laser = new Laser(PositionX + 2, PositionY + 5);
        }

        public void goRight()
        {
            PositionX++;
        }

        public void goLeft()
        {
            if(PositionX == 0)
            {
                throw new Exception("Erreur vous êtes le plus à gauche possible");
            }
            else
            {
                PositionX--;
            }
        }

        public void loseLife()
        {
            Life -= 1;
        }
    }
}