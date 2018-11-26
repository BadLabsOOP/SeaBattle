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
    public class OneHitStategyTests
    {
        [TestMethod()]
        public void ShootTest()
        {
            IComputerStrategy cs = new OneHitStategy();
            ShotsBoardCellState[,] table = new ShotsBoardCellState[,] {
                { ShotsBoardCellState.Missed, ShotsBoardCellState.Missed, ShotsBoardCellState.Killed },
                { ShotsBoardCellState.Unknown, ShotsBoardCellState.Missed, ShotsBoardCellState.Killed },
                { ShotsBoardCellState.Wounded, ShotsBoardCellState.Missed, ShotsBoardCellState.Unknown }
            };
            Assert.IsTrue(cs.Shoot(table).y == 0 && cs.Shoot(table).x == 1);
        }
    }
}