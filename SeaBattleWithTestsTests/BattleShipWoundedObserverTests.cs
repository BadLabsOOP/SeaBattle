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
    public class BattleShipWoundedObserverTests
    {

        [TestMethod()]
        public void OnCompletedTest()
        {
            IObserver<Ship> obs = new BattleShipWoundedObserver(new Ship(1,1,1,1,Player.player1));
            Assert.ThrowsException<NotImplementedException>(() => obs.OnCompleted());
        }

        [TestMethod()]
        public void OnErrorTest()
        {
            IObserver<Ship> obs = new BattleShipWoundedObserver(new Ship(1, 1, 1, 1, Player.player1));
            Assert.ThrowsException<NotImplementedException>(() => obs.OnError(new Exception()));
        }

        [TestMethod()]
        public void OnNextTest()
        {
            IObserver<Ship> obs = new BattleShipWoundedObserver(new Ship(1, 1, 1, 1, Player.player1));
            obs.OnNext(new Ship(1, 1, 1, 1, Player.player2));
        }
    }
}