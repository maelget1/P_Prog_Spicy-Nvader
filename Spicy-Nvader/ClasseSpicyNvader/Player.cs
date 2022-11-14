namespace ClasseSpicyNvader
{
    public class Player : Entity
    {

        public string name;

        private int score;

        public Player(string name, int score, int positionX, int positionY, byte life)
        {
            Skin = @"        ▄        ¦       ███       ¦  ▄███████████▄  ¦  █████████████  ¦  █████████████  ";

            this.name = name;

            Score = score;

            PositionX = positionX;

            PositionY = positionY;

            Life = life;

        }

        public int Score { get => score; set => score = value; }

        public Laser Attack()
        {
            Laser laser = new Laser(PositionX + 8, PositionY - 1);
            return laser;
        }

        public void GoRight()
        {
            if(PositionX == 223)
            {
                
            }
            else
            {
                PositionX++;
            }
        }

        public void GoLeft()
        {
            if (PositionX == 0)
            {
                
            }
            else
            {
                PositionX--;
            }
        }

        public void LoseLife()
        {
            Life -= 1;
        }
    }
}