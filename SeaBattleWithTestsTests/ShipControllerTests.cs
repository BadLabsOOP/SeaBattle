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
    public class ShipControllerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            ShipController sc = new ShipController();
            sc.Add(new Player1ShipBuilder(1, 1, 3, 1));
            sc.Add(new Player2ShipBuilder(1, 1, 3, 1));
            Assert.ThrowsException<Exception>(()=>sc.Add(new Player1ShipBuilder(2,2,2,2)));
            Assert.ThrowsException<Exception>(() => sc.Add(new Player2ShipBuilder(2, 2, 2, 2)));
        }

        [TestMethod()]
        public void ShootTest()
        {
            ShipController sc = new ShipController();
            sc.Add(new Player1ShipBuilder(1, 1, 3, 1));
            Assert.IsTrue(sc.Shoot(2, 1, Player.player2) == ShootResult.Wound);
            Assert.IsTrue(sc.Shoot(2, 1, Player.player1) == ShootResult.Miss);
            Assert.IsTrue(sc.Shoot(1, 1, Player.player2) == ShootResult.Wound);
            Assert.IsTrue(sc.Shoot(3, 1, Player.player2) == ShootResult.Kill);
        }

        [TestMethod()]
        public void P1WinTest()
        {
            ShipController sc = new ShipController();
            sc.Add(new Player2ShipBuilder(1, 1, 1, 1));
            Assert.IsFalse(sc.P1Win());
            sc.Shoot(1, 1, Player.player1);
            Assert.IsTrue(sc.P1Win());
        }

        [TestMethod()]
        public void P2WinTest()
        {
            ShipController sc = new ShipController();
            sc.Add(new Player1ShipBuilder(1, 1, 1, 1));
            Assert.IsFalse(sc.P2Win());
            sc.Shoot(1, 1, Player.player2);
            Assert.IsTrue(sc.P2Win());
        }

        [TestMethod()]
        public void SubscribeTest()
        {
            ShipController sp = new ShipController();
            sp.Subscribe(Game.getInstance() as IObserver<Cell>);
        }

    }
}