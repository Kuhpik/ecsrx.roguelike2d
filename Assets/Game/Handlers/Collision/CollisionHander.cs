using EcsRx.Entities;
using Game.Services;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public abstract class CollisionHander
    {
        protected readonly IEventSystem _eventSystem;
        protected readonly EntityCollisionListener _collistionListener;

        public CollisionHander(IEventSystem eventSystem, EntityCollisionListener collisionListener)
        {
            _eventSystem = eventSystem;
            _collistionListener = collisionListener;

            collisionListener.Subscribe(this);
        }

        public abstract string Tag { get; }
        public abstract void OnCollision(IEntity other, IEntity self);
    }
}
