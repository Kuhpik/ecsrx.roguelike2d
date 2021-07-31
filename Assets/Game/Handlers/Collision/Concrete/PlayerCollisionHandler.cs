using EcsRx.Entities;
using Game.Events;
using Game.Services;
using System.Collections.Generic;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class PlayerCollisionHandler : CollisionHander
    {
        protected override List<string> _tags => new List<string>() { "Player" };

        public PlayerCollisionHandler(IEventSystem eventSystem, EntityCollisionListener collisionListener)
            : base(eventSystem, collisionListener) { }

        public override void OnCollision(IEntity player, IEntity enemy)
        {
            _eventSystem.Publish(new PlayerHitEvent(player, enemy));
        }
    }
}