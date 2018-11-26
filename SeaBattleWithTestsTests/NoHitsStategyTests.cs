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
    public class NoHitsStategyTests
    {
        [TestMethod()]
        public void ShootTest()
        {
            IComputerStrategy cs = new NoHitsStategy();
            ShotsBoardCellState[,] table = new ShotsBoardCellState[,] {
                { ShotsBoardCellState.Missed, ShotsBoardCellState.Missed, ShotsBoardCellState.Killed },
                { ShotsBoardCellState.Missed, ShotsBoardCellState.Missed, ShotsBoardCellState.Killed },
                { ShotsBoardCellState.Wounded, ShotsBoardCellState.Wounded, ShotsBoardCellState.Unknown }
            };
            Assert.IsTrue(cs.Shoot(table).x == 2 && cs.Shoot(table).y == 2);
        }
    }
}