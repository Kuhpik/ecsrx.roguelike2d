using EcsRx.Entities;
using Game.Services;
using System.Collections.Generic;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public abstract class CollisionHander
    {
        protected readonly IEventSystem _eventSystem;
        protected readonly EntityCollisionListener _collistionListener;
        protected abstract List<string> _tags { get; }

        public CollisionHander(IEventSystem eventSystem, EntityCollisionListener collisionListener)
        {
            _eventSystem = eventSystem;
            _collistionListener = collisionListener;

            Tags = _tags.AsReadOnly();
            collisionListener.Subscribe(this);
        }

        public IReadOnlyCollection<string> Tags { get; }
        public abstract void OnCollision(IEntity other, IEntity self);
    }
}
