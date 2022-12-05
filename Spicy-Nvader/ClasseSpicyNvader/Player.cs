namespace ClasseSpicyNvader
{
    public class Player : Entity
    {

        private string name;

        private int score;

        public Player(string name, int positionX, int positionY, byte life)
        {
            Skin = @"      ▄      ¦     ███     ¦▄███████████▄¦█████████████¦█████████████";

            this.Name = name;

            Score = 0;

            PositionX = positionX;

            PositionY = positionY;

            Life = life;

            Width = Skin.Split("¦")[0].Length;

            Height = Skin.Split("¦").Length;
        }

        public int Score { get => score; set => score = value; }
        public string Name { get => name; set => name = value; }

        public Laser Attack()
        {
            Laser laser = new Laser(PositionX + 6, PositionY - 1);
            return laser;
        }

        public void GoRight()
        {
            if(PositionX != 223)
            {
                Console.MoveBufferArea(PositionX, PositionY, Width, Height, ++PositionX, PositionY);
            }
        }

        public void GoLeft()
        {
            if (PositionX != 0)
            {
                Console.MoveBufferArea(PositionX, PositionY, Width, Height, --PositionX, PositionY);
            }
        }

        public void LoseLife()
        {
            Life--;
        }

        public void AddScore()
        {
            Score += 100;
        }
    }
}