using ClasseSpicyNvader;

namespace TestSpicyNvader
{
    [TestClass]
    public class TestPlayer
    {
        [TestMethod]
        public void goRightTest()
        {
            Player player = new Player("Kevin", 0, 0, 0, 3);

            player.goRight();

            Assert.AreEqual(1, player.PositionX);
        }

        [TestMethod]
        public void goLeftTest()
        {
            Player player = new Player("Kevin", 0, 4, 0, 3);

            player.goLeft();

            Assert.AreEqual(3, player.PositionX);
        }

        [TestMethod]
        public void goLeftTest2()
        {
            Player player = new Player("Quentin", 0, 0, 0, 3);

            bool validation = true;
            try
            {
                player.goLeft();
                validation = false;
            }
            catch
            {

            }

            Assert.IsTrue(validation);
        }

        [TestMethod]
        public void constructTest()
        {
            Player player = new Player("Quentin", 0, 0, 0, 3);

            Assert.AreEqual(0, player.PositionX);

            Assert.AreEqual(0, player.PositionY);

            Assert.AreEqual("Quentin", player.name);

            Assert.AreEqual(3, player.Life);

            Assert.AreEqual(0, player.Score);
        }
    }
}