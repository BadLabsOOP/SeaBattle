using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    public static class ComputerShipPlacer
    {
        static Random Random = new Random();

        public static void Place(int curShipLength, int width, int height, ShipController ShipController)
        {
            bool isPlaced = false;
            while (!isPlaced)
            {
                try
                {
                    var direction = Random.Next(0, 3);
                    var x = Random.Next(0, width - 1);
                    var y = Random.Next(0, height - 1);
                    switch (direction)
                    {
                        case 0:
                            CriticalCheck(x - curShipLength < 0);
                            CreateShip(x - curShipLength, y, x, y, ShipController);
                            break;
                        case 1:
                            CriticalCheck(y - curShipLength < 0);
                            CreateShip(x, y - curShipLength, x, y, ShipController);
                            break;
                        case 2:
                            CriticalCheck(x + curShipLength > width);
                            CreateShip(x, y, x + curShipLength, y, ShipController);
                            break;
                        case 3:
                            CriticalCheck(y + curShipLength > height);
                            CreateShip(x, y, x, y + curShipLength, ShipController);
                            break;
                    }
                    isPlaced = true;
                }
                catch { }
            }
        }
        private static void CreateShip(int x1, int y1, int x2, int y2, ShipController ShipController)
        {
            ShipController.Add(new Player2ShipBuilder(x1, y1, x2, y2));
        }
        private static void CriticalCheck(bool check)
        {
            if (check) throw new Exception();
        }
    }
}
