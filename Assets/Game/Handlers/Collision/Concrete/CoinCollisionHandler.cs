using EcsRx.Entities;
using EcsRx.Extensions;
using Game.Components;
using Game.Events;
using Game.Services;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class CoinCollisionHandler : CollisionHander
    {
        public override string Tag => "Coin";

        public CoinCollisionHandler(IEventSystem eventSystem, EntityCollisionListener collisionListener)
            : base(eventSystem, collisionListener) { }

        public override void OnCollision(IEntity coin, IEntity player)
        {
            if (player.HasComponent<PlayerComponent>())
            {
                _eventSystem.Publish(new CoinPickupEvent(coin, player));
            }
        }
    }
}