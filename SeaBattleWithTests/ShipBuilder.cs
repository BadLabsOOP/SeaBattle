namespace SeaBattle
{
    public interface ShipBuilder
    {
        Ship Build();
    }
    public class Player1ShipBuilder : ShipBuilder
    {
        Ship ship;
        public Player1ShipBuilder(int startX, int startY, int endX, int endY)
        {
            ship = new Ship(startX, startY, endX, endY, Player.player1);
        }

        public Ship Build()
        {
            return ship;
        }
    }
    public class Player2ShipBuilder : ShipBuilder
    {
        Ship ship;
        public Player2ShipBuilder(int startX, int startY, int endX, int endY)
        {
            ship = new Ship(startX, startY, endX, endY, Player.player2);
        }

        public Ship Build()
        {
            return ship;
        }
    }
}