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
    public class ShipTests
    {
        [TestMethod()]
        public void TryHitMeTest()
        {
            Ship ship = new Ship(1, 1, 3, 1, Player.player1);
            Assert.IsTrue(ship.TryHitMe(1, 1));
            Assert.IsFalse(ship.TryHitMe(1, 2));
        }

        [TestMethod()]
        public void CollideTest()
        {
            Ship ship = new Ship(1, 1, 3, 1, Player.player1);
            Ship shipCollides = new Ship(4, 1, 4, 1, Player.player1);
            Ship shipNotCollides = new Ship(5, 1, 5, 1, Player.player1);
            Assert.IsTrue(ship.Collide(shipCollides));
            Assert.IsTrue(shipCollides.Collide(shipCollides));
            Assert.IsTrue(shipCollides.Collide(ship));
            Assert.IsFalse(ship.Collide(shipNotCollides));
            Assert.IsFalse(shipNotCollides.Collide(ship));
        }


        [TestMethod()]
        public void ToStringTest()
        {
            Ship ship = new Ship(1, 1, 3, 1, Player.player1);
            Assert.AreEqual($"from {ship.StartX},{ship.StartY} to {ship.EndX},{ship.EndY}", ship.ToString());
        }
    }
}