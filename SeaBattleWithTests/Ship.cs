using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    public class Ship
    {
        private readonly bool[,] cells;
        public int StartX { get;  }
        public int StartY { get;  }
        public int EndX { get; }
        public int EndY { get;  }
        public Player Player { get; }
        public ShipStatus Status { get; private set; }
        public bool TryHitMe(int x, int y) {
            if (x >= StartX && x <= EndX && y >= StartY && y <= EndY)
            {
                if (cells[x - StartX , y - StartY]) throw new Exception("Already shot");
                cells[EndX != StartX ? x - StartX : 0, EndY != StartY ? y - StartY : 0] = true;
                Status = ShipStatus.Wounded;
                foreach (var cell in cells)
                {
                    if (!cell) return true;
                }
                Status = ShipStatus.Dead;
                return true;
            }
            return false;
        }
        public bool Collide(Ship ship)
        {
         if (StartX-1 > ship.EndX ||
         EndX+1 < ship.StartX ||
         StartY-1 > ship.EndY ||
         EndY+1 < ship.StartY)  return false;
         return true;
        }
        public Ship(int startX, int startY, int endX, int endY, Player player)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            Player = player;
            Status = ShipStatus.Live;
            cells = new bool[EndX - StartX+1, EndY - StartY +1];
        }
        public override string ToString()
        {
            return $"from {StartX},{StartY} to {EndX},{EndY}";
        }

    }
}
