using ClasseSpicyNvader;

namespace TestSpicyNvader
{
    [TestClass]
    public class TestPlayer
    {
        [TestMethod]
        public void goRightTest()
        {
            Player player = new Player("test", 0, 0, 0);
            bool succes;

            try
            {
                player.GoRight();
                succes = true;
            }
            catch
            {
                succes = false;
            }


            Assert.IsTrue(succes);
        }

        [TestMethod]
        public void goLeftTest()
        {
            Player player = new Player("test", 4, 0, 0);
            bool succes;

            try
            {
                player.GoLeft();
                succes = true;
            }
            catch
            {
                succes = false;
            }
            

            Assert.IsTrue(succes);
        }

        [TestMethod]
        public void goLeftTest2()
        {
            Player player = new Player("test", 0, 0, 0);

            player.GoLeft();

            Assert.AreEqual(0, player.PositionX);
        }

        [TestMethod]
        public void constructTest()
        {
            Player player = new Player("Quentin", 0, 0, 0);

            Assert.AreEqual(0, player.PositionX);

            Assert.AreEqual(0, player.PositionY);

            Assert.AreEqual("Quentin", player.Name);

            Assert.AreEqual(0, player.Life);

            Assert.AreEqual(0, player.Score);
        }
    }
}