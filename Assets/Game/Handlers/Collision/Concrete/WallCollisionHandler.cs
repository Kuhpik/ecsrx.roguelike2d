using EcsRx.Entities;
using EcsRx.Extensions;
using Game.Components;
using Game.Events;
using Game.Services;
using System.Collections.Generic;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class WallCollisionHandler : CollisionHander
    {
        protected override List<string> _tags => new List<string>() { "Wall" };

        public WallCollisionHandler(IEventSystem eventSystem, EntityCollisionListener collisionListener)
            : base(eventSystem, collisionListener) { }

        public override void OnCollision(IEntity wall, IEntity player)
        {
            if (player.HasComponent<PlayerComponent>()) //Enemies can't break our walls
            {
                _eventSystem.Publish(new WallHitEvent(wall, player));
            }
        }
    }
}