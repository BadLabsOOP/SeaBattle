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
    public class Player1ShipBuilderTests
    {

        [TestMethod()]
        public void BuildTest()
        {
            ShipBuilder sb = new Player1ShipBuilder(1, 1, 10, 1);
            Assert.IsTrue(sb.Build().StartX == 1);
            Assert.IsTrue(sb.Build().StartY == 1);
            Assert.IsTrue(sb.Build().EndX == 10);
            Assert.IsTrue(sb.Build().EndY == 1);
            Assert.IsTrue(sb.Build().Status == ShipStatus.Live);
        }
    }
}