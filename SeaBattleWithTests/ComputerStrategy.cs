using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    public interface IComputerStrategy
    {
        Cell Shoot(ShotsBoardCellState[,] table);
    }
    public class NoHitsStategy : IComputerStrategy
    {
        public Cell Shoot(ShotsBoardCellState[,] table)
        {
            Random random = new Random();
            while (true)
            {
                var x = random.Next(0,table.GetLength(0));
                var y = random.Next(0, table.GetLength(1));
                if (table[x, y] == ShotsBoardCellState.Unknown) return new Cell() {x=x,y=y };
            }
        }
    }
    public class OneHitStategy : IComputerStrategy
    {
        public virtual Cell Shoot(ShotsBoardCellState[,] table)
        {
            Cell cell = FindRightBottomWound(table);
            var x = cell.x;
            var y = cell.y;
            if (!(x - 1 < 0) && table[x - 1, y] == ShotsBoardCellState.Unknown) return new Cell() { x = x - 1, y = y };
            if (!(y - 1 < 0) && table[x, y - 1] == ShotsBoardCellState.Unknown) return new Cell() { x = x, y = y - 1 };
            if (!(x + 1 > table.GetLength(0)) && table[x + 1, y] == ShotsBoardCellState.Unknown) return new Cell() { x = x + 1, y = y };
            return new Cell() { x = x, y = y + 1 };
        }

        protected static Cell FindRightBottomWound(ShotsBoardCellState[,] table)
        {
            Cell cell = new Cell() { x = -1, y = -1 };
            for (int i = 0; i < table.GetLength(0); i++)
                for (int j = 0; j < table.GetLength(1); j++)
                    if (table[i, j] == ShotsBoardCellState.Wounded) cell = new Cell() { x = i, y = j };
            if (cell.x == -1) throw new Exception();
            return cell;
        }
    }
    public class ManyHitsStategy : OneHitStategy
    {
        public override Cell Shoot(ShotsBoardCellState[,] table)
        {
            Cell cell = FindRightBottomWound(table);
            var x = cell.x;
            var y = cell.y;
            if (!(x - 1 < 0) && table[x - 1, y] == ShotsBoardCellState.Wounded)
            {
                int i = 2;
                while (!(x - i < 0))
                {
                    if (table[x - i, y] == ShotsBoardCellState.Unknown) return new Cell() { x = x - i, y = y };
                    if (table[x - i, y] == ShotsBoardCellState.Missed) break;
                    i++;
                }
                i = 1;
                while (!(x + i > table.GetLength(0)))
                {
                    if (table[x + i, y] == ShotsBoardCellState.Unknown) return new Cell() { x = x + i, y = y };
                    if (table[x + i, y] == ShotsBoardCellState.Missed) break;
                    i++;
                }
            }
            if (!(y - 1 < 0) && table[y - 1, y] == ShotsBoardCellState.Wounded)
            {
                int i = 2;
                while (!(y - i < 0))
                {
                    if (table[x, y - i] == ShotsBoardCellState.Unknown) return new Cell() { x = x, y = y - i };
                    if (table[x, y - i] == ShotsBoardCellState.Missed) break;
                    i++;
                }
                i = 1;
                while (!(y + i > table.GetLength(1)))
                {
                    if (table[x, y + i] == ShotsBoardCellState.Unknown) return new Cell() { x = x, y = y + i };
                    if (table[x, y + i] == ShotsBoardCellState.Missed) break;
                    i++;
                }
            }

            throw new Exception();
        }
    }
}
