using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void GetWidthTest()
        {
            Game g = Game.getInstance();
            g.SetWidth(3);
            Assert.IsTrue(g.GetWidth() == 3);
        }

        [TestMethod()]
        public void GetHeightTest()
        {
            Game g = Game.getInstance();
            g.SetHeight(3);
            Assert.IsTrue(g.GetHeight() == 3);
        }

        [TestMethod()]
        public void getInstanceTest()
        {
            Game g1 = Game.getInstance();
            Game g2 = Game.getInstance();
            Assert.AreEqual(g1, g2);
        }

        [TestMethod()]
        public void OnCompletedTest()
        {
            IObserver<Ship> obs = Game.getInstance();
            Assert.ThrowsException<NotImplementedException>(() => obs.OnCompleted());
        }

        [TestMethod()]
        public void OnErrorTest()
        {
            IObserver<Ship> obs = Game.getInstance();
            Assert.ThrowsException<NotImplementedException>(() => obs.OnError(new Exception()));
        }

    }
}