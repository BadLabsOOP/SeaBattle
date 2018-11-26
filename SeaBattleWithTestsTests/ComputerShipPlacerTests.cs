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
    public class ComputerShipPlacerTests
    {
        [TestMethod()]
        public void PlaceTest()
        {
            ShipController sc = new ShipController();
            sc.Add(new Player1ShipBuilder(1, 1, 3, 1));
            sc.Add(new Player2ShipBuilder(1, 1, 3, 1));
            ComputerShipPlacer.Place(3, 10, 10, sc);
        }
    }
}