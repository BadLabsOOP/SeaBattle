using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    
    public class BattleShipWoundedObserver:IObserver<Ship>
    {
        Ship ship;
        public BattleShipWoundedObserver(Ship _ship) {
            ship = _ship;
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
            if (ship.Status==ShipStatus.Wounded)
            Console.WriteLine($"Ship of {ship.Player} was wounded");
            if (ship.Status == ShipStatus.Dead)
                Console.WriteLine($"Ship {ship.Player} became dead");
        }
    }
}
