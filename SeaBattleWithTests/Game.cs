using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    public class Game: IObserver<Ship>, IObserver<Cell>
    {
        public GameStatus GameStatus { get; private set; } = GameStatus.boardSetting;
        public int GetWidth() => width;
        public int GetHeight() => height;
        int width, height;

        internal void NextTurn()
        {
            if (getInstance().GameStatus == GameStatus.shipPlacing) getInstance().GameStatus = GameStatus.shootingP1;
            else if(getInstance().GameStatus == GameStatus.boardSetting) getInstance().GameStatus = GameStatus.shipPlacing;
            else if(getInstance().GameStatus == GameStatus.shootingP1) getInstance().GameStatus = GameStatus.shootingP2;
            else if (getInstance().GameStatus == GameStatus.shootingP2) getInstance().GameStatus = GameStatus.shootingP1;
        }
        internal void EndGame(Player winner)
        {
            Console.WriteLine($"Player {winner} won! Congratulations!");
            getInstance().GameStatus = GameStatus.end;
        }
        ShotsBoardCellState[,] player1shots, player2shots;
        private static Game instance;
        void reinit() {
            player1shots = new ShotsBoardCellState[height, width];
            player2shots = new ShotsBoardCellState[height, width];
        }
        public void SetWidth(int newWidth) {
            getInstance().width = newWidth;
            getInstance().reinit();
        }
        public void SetHeight(int newHeight)
        {
            getInstance().height = newHeight;
            getInstance().reinit();
        }

        internal ShotsBoardCellState[,] GetShotsTable(Player player)
        {
            var table = player == Player.player1 ? getInstance().player1shots : getInstance().player2shots;
            return table;
            
        }
        private Game()
        { }
        
        public static Game getInstance()
        {
            if (instance == null)
            {
                instance = new Game();
                instance.reinit();
            }
            return instance;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Ship value)
        {
            if (value.Status == ShipStatus.Wounded)
            {
                Console.WriteLine($"Player {value.Player} wounded ship!");
            }
            if (value.Status == ShipStatus.Dead)
            {
                Console.WriteLine($"Player {value.Player} destroyed ship!");
            }
        }


        public void OnNext(Cell value)
        {
            try
            {
                var table = value.player == Player.player1 ? getInstance().player1shots : player2shots;
                Console.WriteLine($"Player {value.player} shot ({value.x},{value.y})");
                if (value.shootResult == ShootResult.Wound)
                {
                    table[value.x, value.y] = ShotsBoardCellState.Wounded;
                }
                if (value.shootResult == ShootResult.Kill)
                    table[value.x, value.y] = ShotsBoardCellState.Killed;
                if (value.shootResult == ShootResult.Miss)
                    table[value.x, value.y] = ShotsBoardCellState.Missed;
            }
            catch { }
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<GameStatus>> _observers;
            private IObserver<GameStatus> _observer;

            public Unsubscriber(List<IObserver<GameStatus>> observers, IObserver<GameStatus> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
