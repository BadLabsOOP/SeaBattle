using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattle
{
    public class ShipController:IObservable<Ship>, IObservable<Cell>
    {
        readonly List<Ship> ships = new List<Ship>();
        private List<IObserver<Ship>> observers=new List<IObserver<Ship>>();
        private List<IObserver<Cell>> gameObservers = new List<IObserver<Cell>>();
        public void Add(ShipBuilder shipBuilder) {
            var ship = shipBuilder.Build();
            foreach (var s in ships.Where(x => x.Player == ship.Player).ToList())
                if (s.Collide(ship)) throw new Exception();
            ships.Add(ship);
            this.Subscribe(new BattleShipWoundedObserver(ship));
        }
        public ShootResult Shoot(int x, int y, Player player)
        {
            var wounded = ships.FirstOrDefault(ship => ship.Player != player && ship.TryHitMe(x, y));
            if (wounded == null) {
                foreach (var obs in gameObservers)
                    obs.OnNext(new Cell() {player=player, shootResult=ShootResult.Miss, x=x,y=y });
                return ShootResult.Miss;
            }
            if (wounded.Status == ShipStatus.Wounded)
            {
                foreach (var obs in observers)
                    obs.OnNext(wounded);
                foreach (var obs in gameObservers)
                    obs.OnNext(new Cell() { player = player, shootResult = ShootResult.Wound, x = x, y = y });
                return ShootResult.Wound;
            }
            foreach (var obs in observers)
                obs.OnNext(wounded);
            foreach (var obs in gameObservers)
                for (int i=wounded.StartX;i<=wounded.EndX;i++)
                    for (int j = wounded.StartY; j <= wounded.EndY; j++)
                        obs.OnNext(new Cell() { player = player, shootResult = ShootResult.Kill, x = i, y = j });
            return ShootResult.Kill;
        }
        private class Unsubscriber<T> : IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
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

        public bool P1Win() {
            return !ships.Any(x => x.Status!= ShipStatus.Dead && x.Player==Player.player2);
        }
        public bool P2Win()
        {
            return !ships.Any(x => x.Status != ShipStatus.Dead && x.Player == Player.player1);
        }
        internal void ShowField(Player player,int width, int height)
        {
            bool[,] field = new bool[width, height];
            foreach (var ship in ships.Where(x => x.Player == player).ToList())
            {
                for (int j = ship.StartY; j <= ship.EndY; j++)
                {
                    for (int i = ship.StartX; i <= ship.EndX; i++)
                    {
                        field[i, j] = true;
                    }
                    
                }
            }
            for (int j = 0; j < height; j++)
            {
                Console.Write("|");
                for (int i = 0; i < width; i++)
                {
                    Console.Write(field[i, j] ? "X" : " ");
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        public IDisposable Subscribe(IObserver<Ship> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<Ship>(observers, observer);
        }

        public IDisposable Subscribe(IObserver<Cell> observer)
        {
            if (!gameObservers.Contains(observer))
                gameObservers.Add(observer);
            return new Unsubscriber<Cell>(gameObservers, observer);
        }
    }
}
