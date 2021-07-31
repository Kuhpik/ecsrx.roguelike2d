using EcsRx.Entities;
using EcsRx.Extensions;
using Game.Components;
using Game.Events;
using Game.Services;
using System.Collections.Generic;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class CoinCollisionHandler : CollisionHander
    {
        protected override List<string> _tags => new List<string>() { "Coin" };

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