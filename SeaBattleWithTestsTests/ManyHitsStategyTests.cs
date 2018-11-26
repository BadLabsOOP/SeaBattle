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
    public class ManyHitsStategyTests
    {
        [TestMethod()]
        public void ShootTest()
        {
            IComputerStrategy cs = new ManyHitsStategy();
            ShotsBoardCellState[,] table = new ShotsBoardCellState[,] {
                { ShotsBoardCellState.Unknown, ShotsBoardCellState.Missed, ShotsBoardCellState.Killed },
                { ShotsBoardCellState.Wounded, ShotsBoardCellState.Unknown, ShotsBoardCellState.Killed },
                { ShotsBoardCellState.Wounded, ShotsBoardCellState.Unknown, ShotsBoardCellState.Unknown }
            };
            Assert.IsTrue(cs.Shoot(table).y == 0 && cs.Shoot(table).x == 0);
        }
    }
}