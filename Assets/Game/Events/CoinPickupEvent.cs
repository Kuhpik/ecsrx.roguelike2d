using EcsRx.Entities;

namespace Game.Events
{
    public class CoinPickupEvent
    {
        public IEntity Player { get; private set; }
        public IEntity Coin { get; private set; }

        public CoinPickupEvent(IEntity coin, IEntity player)
        {
            Player = player;
            Coin = coin;
        }
    }
}