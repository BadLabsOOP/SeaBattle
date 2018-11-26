using System;
using System.Collections.Generic;

namespace SeaBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            var shipController = new ShipController();
            var game = Game.getInstance();
            shipController.Subscribe(game as IObserver<Cell>);
            Stack<int> shipsLeft = new Stack<int>();
            var tmp = new List<int> { 1,2,3 };
            FillStack(shipsLeft,tmp);

            Console.WriteLine("This is sea battle");
            while (input.ToUpper() != "EXIT")
            {
                switch (game.GameStatus)
                {
                    case GameStatus.boardSetting:
                        const int minWidth= 3, minHeight = 3,maxWidth=20,maxHeight=20;
                        game.SetWidth(CheckedInput($"width ({minWidth}-{maxWidth}) =", minWidth, maxWidth));
                        game.SetHeight(CheckedInput($"height ({minHeight}-{maxHeight}) =", minHeight, maxHeight));
                        game.NextTurn();
                        break;
                    case GameStatus.shipPlacing:
                        int curShipLength = 0;
                        try
                        {
                            curShipLength = shipsLeft.Pop();
                        }
                        catch
                        {
                            game.NextTurn();
                            continue;
                        }
                        ComputerShipPlacer.Place(curShipLength-1, game.GetWidth(), game.GetHeight(), shipController);
                        bool isPlacingCorrect = false;
                        while (!isPlacingCorrect)
                        {

                            Console.WriteLine($"Place ship with length ={curShipLength}");
                            shipController.ShowField(Player.player1, game.GetWidth(), game.GetHeight());
                            int x = CheckedInput("x=", 0, game.GetWidth() - 1);
                            int y = CheckedInput("y=", 0, game.GetHeight() - 1);
                            int direction = 4;
                            direction = CheckedInput("Enter direction: 0=left, 1=up, 2=right, 3= down", 0, 3);
                            Player1ShipBuilder player1ShipBuilder = null;
                            curShipLength--;
                            try
                            {
                                switch (direction)
                                {
                                    case 0:
                                        if (x - curShipLength < 0) throw new Exception();
                                        player1ShipBuilder = new Player1ShipBuilder(x - curShipLength, y, x, y);
                                        break;
                                    case 1:
                                        if (y - curShipLength < 0) throw new Exception();
                                        player1ShipBuilder = new Player1ShipBuilder(x, y - curShipLength, x, y);
                                        break;
                                    case 2:
                                        if (x + curShipLength > game.GetWidth()) throw new Exception();
                                        player1ShipBuilder = new Player1ShipBuilder(x, y, x + curShipLength, y);
                                        break;
                                    case 3:
                                        if (y + curShipLength > game.GetHeight()) throw new Exception();
                                        player1ShipBuilder = new Player1ShipBuilder(x, y, x, y + curShipLength);
                                        break;
                                }

                                shipController.Add(player1ShipBuilder);
                                isPlacingCorrect = true;
                            }
                            catch
                            {
                                Console.WriteLine("Incorrect placing");
                                curShipLength++;
                            }
                        }
                        break;
                    case GameStatus.shootingP1:
                        var table = game.GetShotsTable(Player.player1);
                        var isCorrectShot = false;
                        while (!isCorrectShot)
                            try
                            {
                                Console.WriteLine("You shoot now! Your shots:");
                                PrintShots(game, table);
                                int xTmp = CheckedInput("x=", 0, game.GetWidth() - 1);
                                int yTmp = CheckedInput("y=", 0, game.GetHeight() - 1);
                                if (table[xTmp, yTmp] != ShotsBoardCellState.Unknown) throw new Exception();
                                var playerShotResult = shipController.Shoot(xTmp, yTmp, Player.player1);
                                Console.WriteLine(playerShotResult);
                                if (playerShotResult == ShootResult.Miss) isCorrectShot = true;
                                if (shipController.P1Win()) { game.EndGame(Player.player1); break; }
                            }
                            catch
                            {
                                Console.WriteLine("Incorrect shot!");
                            }
                        game.NextTurn();

                        break;
                    case GameStatus.shootingP2:
                        var comShotsTable = game.GetShotsTable(Player.player2);
                        Console.WriteLine("Computer shoots now! Computer shots:");
                        PrintShots(game, comShotsTable);
                        var iscCorrectShot = false;
                        IComputerStrategy computerStrategy = null;
                        while (!iscCorrectShot)
                        {
                            try
                            {
                                var woundsCount = 0;
                                foreach (ShotsBoardCellState c in comShotsTable)
                                    if (c == ShotsBoardCellState.Wounded) woundsCount++;
                                if (woundsCount == 0) computerStrategy = new NoHitsStategy();
                                if (woundsCount == 1) computerStrategy = new OneHitStategy();
                                if (woundsCount > 1) computerStrategy = new ManyHitsStategy();
                                var shotCell=computerStrategy.Shoot(comShotsTable);
                                var computerShotResult = shipController.Shoot(shotCell.x, shotCell.y, Player.player2);
                                Console.WriteLine("Computer shot result" + computerShotResult);
                                if (computerShotResult == ShootResult.Miss) iscCorrectShot = true;
                                if (shipController.P2Win()) { game.EndGame(Player.player2); break; }
                            }
                            catch { }
                        }
                        game.NextTurn();
                        break;
                    case GameStatus.end:
                        Console.WriteLine("Game ended. type 'exit' to exit");
                        input = Console.ReadLine();
                        break;
                }
            }
        }

        private static void PrintShots(Game game, ShotsBoardCellState[,] table)
        {
            for (int j = 0; j < game.GetHeight(); j++)
            {
                Console.Write("|");
                for (int i = 0; i < game.GetWidth(); i++)
                {
                    if (table[i, j] == ShotsBoardCellState.Unknown) Console.Write(" ");
                    if (table[i, j] == ShotsBoardCellState.Wounded) Console.Write("O");
                    if (table[i, j] == ShotsBoardCellState.Missed) Console.Write("'");
                    if (table[i, j] == ShotsBoardCellState.Killed) Console.Write("X");
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        private static void FillStack(Stack<int> shipsLeft, List<int> shipsToFill)
        {
            foreach (var s in shipsToFill)
                shipsLeft.Push(s);
        }

        private static int CheckedInput(string msg, int from=int.MinValue, int to=int.MaxValue)
        {
            bool isValid = false;
            int res=0;
            while (!isValid)
                try
                {
                    Console.WriteLine(msg);
                    Console.WriteLine($"Value must be from {from} to {to}");
                    res = Convert.ToInt32(Console.ReadLine());
                    if (res < from || res > to) throw new Exception();
                    isValid = true;
                }
                catch
                {
                    Console.WriteLine("INVALID!");
                }
            return res;
        }
    }
}
