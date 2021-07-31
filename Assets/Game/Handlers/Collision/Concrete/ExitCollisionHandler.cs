using EcsRx.Entities;
using Game.Events;
using Game.Services;
using System.Collections.Generic;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class ExitCollisionHandler : CollisionHander
    {
        protected override List<string> _tags => new List<string>() { "Exit" };

        public ExitCollisionHandler(IEventSystem eventSystem, EntityCollisionListener collisionListener)
            : base(eventSystem, collisionListener) { }

        public override void OnCollision(IEntity exit, IEntity player)
        {
            _eventSystem.Publish(new ExitReachedEvent(exit, player));
        }
    }
}